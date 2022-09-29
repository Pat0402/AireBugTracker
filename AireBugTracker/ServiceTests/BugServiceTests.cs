using DatabaseContext.Models;
using Moq;
using Repsoitories.Interfaces;
using Repsoitories.Respositories;
using Services.Services;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace ServiceTests
{
    public class Tests
    {
        [Test]
        public async Task GetAll_ReturnsAllBugs()
        {
            // Create mock repository
            var bugRepositoryMock = new Mock<IBugRepository>();
            var theBugs = new List<Bug> {
                new Bug
                {
                    Id = 1,
                    Title = "First Bug",
                    Details = "These are the details of the first bug",
                    OpenedDate = DateTimeOffset.UtcNow,
                    IsOpen = true
                },
                new Bug
                {
                    Id = 2,
                    Title = "Second Bug",
                    Details = "These are the details of the second bug",
                    OpenedDate = DateTimeOffset.UtcNow
                },
                new Bug
                {
                    Id = 3,
                    Title = "Third Bug",
                    Details = "These are the details of the third bug",
                    OpenedDate = DateTimeOffset.UtcNow,
                    IsOpen = true
                }
            };

            bugRepositoryMock.Setup(b => b.GetAll()).Returns(Task.FromResult(theBugs));

            // Create the service
            var bugService = new BugService(bugRepositoryMock.Object);

            // Call action
            var result = await bugService.GetAll();

            // Verify
            Assert.That(theBugs, Has.Count.EqualTo(3));

            var firstBug = theBugs.Single(bug => bug.Id == 2);
            Assert.Multiple(() =>
            {
                Assert.That(firstBug.IsOpen, Is.False);
                Assert.That(firstBug.Title, Is.EqualTo("Second Bug"));
                Assert.That(firstBug.Details, Is.EqualTo("These are the details of the second bug"));
                Assert.That(firstBug.OpenedDate, Is.AtMost(DateTimeOffset.UtcNow));
            });

            bugRepositoryMock.Verify(b => b.GetAll(), Times.Once());
        }

        [Test]
        public async Task GetByExistingId_ReturnsBug()
        {
            // Create mock repository
            var bugRepositoryMock = new Mock<IBugRepository>();
            var theBug = new Bug
            {
                Id = 1,
                Title = "First Bug",
                Details = "These are the details of the first bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            };

            bugRepositoryMock.Setup(b => b.GetById(1)).Returns(Task.FromResult(theBug));

            // Create the service
            var bugService = new BugService(bugRepositoryMock.Object);

            // Call action
            var result = await bugService.GetById(1);

            // Verify
            
            Assert.Multiple(() =>
            {
                Assert.That(result.IsOpen, Is.True);
                Assert.That(result.Title, Is.EqualTo("First Bug"));
                Assert.That(result.Details, Is.EqualTo("These are the details of the first bug"));
                Assert.That(result.OpenedDate, Is.AtMost(DateTimeOffset.UtcNow));
            });

            bugRepositoryMock.Verify(b => b.GetById(1), Times.Once());
        }

        [Test]
        public async Task GetByNonExistentId_ReturnsNull()
        {
            // Create mock repository
            var bugRepositoryMock = new Mock<IBugRepository>();
            bugRepositoryMock.Setup(b => b.GetById(4)).Returns(Task.FromResult<Bug>(null));

            // Create the service
            var bugService = new BugService(bugRepositoryMock.Object);

            // Call action
            var result = await bugService.GetById(4);

            // Verify

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Null);
            });

            bugRepositoryMock.Verify(b => b.GetById(4), Times.Once());
        }

        [Test]
        public async Task CreateBug_CreatesBug()
        {
            // Create mock repository
            var bugRepositoryMock = new Mock<IBugRepository>();
            var theBug = new Bug()
            {
                Title = "First Bug",
                Details = "These are the details of the first bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            };

            bugRepositoryMock.Setup(b => b.CreateAsync(It.IsAny<Bug>())).Returns(Task.FromResult<Bug>(theBug));

            // Create the service
            var bugService = new BugService(bugRepositoryMock.Object);

            // Call action
            var result = await bugService.CreateAsync(theBug);

            // Verify

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.Target.IsOpen, Is.True);
                Assert.That(result.Target.Title, Is.EqualTo("First Bug"));
                Assert.That(result.Target.Details, Is.EqualTo("These are the details of the first bug"));
                Assert.That(result.Target.OpenedDate, Is.EqualTo(theBug.OpenedDate));
            });

            bugRepositoryMock.Verify(b => b.CreateAsync(It.IsAny<Bug>()), Times.Once());
        }

        [Test]
        public async Task CreateConflictingBug_IsUnsuccessful()
        {
            // Create mock repository
            var bugRepositoryMock = new Mock<IBugRepository>();
            var theBug = new Bug()
            {
                Title = "First Bug",
                Details = "These are the details of the first bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            };

            bugRepositoryMock.Setup(b => b.CreateAsync(It.IsAny<Bug>()))
                .Throws(new DbUpdateException());

            // Create the service
            var bugService = new BugService(bugRepositoryMock.Object);

            // Verify
            var result = await bugService.CreateAsync(theBug);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.False);
                Assert.That(result.Target, Is.Null);
            });

            bugRepositoryMock.Verify(b => b.CreateAsync(It.IsAny<Bug>()), Times.Once());
        }
    }
}