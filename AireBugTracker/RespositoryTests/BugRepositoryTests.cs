using DatabaseContext;
using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;
using Repsoitories.Respositories;
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
            var connection = Effort.DbConnectionFactory.CreateTransient();
            
            using (var dbContext = new BugTrackerContext(connection))
            {
                dbContext.Bugs.AddRange(new List<Bug> {
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
                });
                await dbContext.SaveChangesAsync();
            }

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
    }
}