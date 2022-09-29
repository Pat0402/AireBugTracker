using DatabaseContext;
using DatabaseContext.Models;
using Effort.Provider;
using Microsoft.EntityFrameworkCore;
using Repsoitories.Respositories;
using RespositoryTests.Helpers;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace RespositoryTests
{
    public class BugRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAll_ReturnsAllBugs()
        {
            // Set up in memory database
            var connection = await TestBugHelper.GetSeededEffortConnection();

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Retrieve the persisted data
                var bugRepository = new BugRepository(dbContext);
                var theBugs = await bugRepository.GetAll();

                // Verify
                Assert.That(theBugs, Has.Count.EqualTo(3));

                var firstBug = theBugs.Single(bug => bug.Id == 1);
                Assert.Multiple(() =>
                {
                    Assert.That(firstBug.IsOpen, Is.True);
                    Assert.That(firstBug.Title, Is.EqualTo("First Bug"));
                    Assert.That(firstBug.Details, Is.EqualTo("These are the details of the first bug"));
                    Assert.That(firstBug.OpenedDate, Is.AtMost(DateTimeOffset.UtcNow));
                });
            }
        }

        [Test]
        public async Task GetByExistingId_ReturnsBug()
        {
            // Set up in memory database
            var connection = await TestBugHelper.GetSeededEffortConnection();

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Retrieve the persisted data
                var bugRepository = new BugRepository(dbContext);
                var theBug = await bugRepository.GetById(2);

                // Verify
                Assert.Multiple(() =>
                {
                    Assert.That(theBug.IsOpen, Is.False);
                    Assert.That(theBug.Title, Is.EqualTo("Second Bug"));
                    Assert.That(theBug.Details, Is.EqualTo("These are the details of the second bug"));
                    Assert.That(theBug.OpenedDate, Is.AtMost(DateTimeOffset.UtcNow));
                });
            }
        }

        [Test]
        public async Task GetByNonExistentId_ReturnsNull()
        {
            var connection = await TestBugHelper.GetSeededEffortConnection();

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Retrieve the persisted data
                var bugRepository = new BugRepository(dbContext);
                var theBug = await bugRepository.GetById(4);

                // Verify
                Assert.Multiple(() =>
                {
                    Assert.That(theBug, Is.Null);
                });
            }
        }

        [Test]
        public async Task CreateBug_CreatesBug()
        {
            var connection = await TestBugHelper.GetSeededEffortConnection();
            var newBug = new Bug
            {
                Id = 4,
                Title = "Forth Bug",
                Details = "These are the details of the forth bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            };

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Persist the new bug
                var bugRepository = new BugRepository(dbContext);
                await bugRepository.CreateAsync(newBug);

                // Retrieve the new bug for the DB
                var retrievedBug = await bugRepository.GetById(newBug.Id);

                // Verify
                Assert.Multiple(() =>
                {
                    Assert.That(retrievedBug.IsOpen, Is.True);
                    Assert.That(retrievedBug.Title, Is.EqualTo(newBug.Title));
                    Assert.That(retrievedBug.Details, Is.EqualTo(newBug.Details));
                    Assert.That(retrievedBug.OpenedDate, Is.EqualTo(newBug.OpenedDate));
                });
            }
        }

        [Test]
        public async Task CreateBugWithExistingTitle_ThrowsException()
        {
            var connection = await TestBugHelper.GetSeededEffortConnection();
            var newBug = new Bug
            {
                Title = "Third Bug",
                Details = "These are the details of the third bug",
                OpenedDate = DateTimeOffset.UtcNow,
                IsOpen = true
            };

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Persist the new bug
                var bugRepository = new BugRepository(dbContext);

                // Verify
                Assert.ThrowsAsync<System.Data.Entity.Infrastructure.DbUpdateException>(
                    async () => await bugRepository.CreateAsync(newBug));
            }
        }
    }
}