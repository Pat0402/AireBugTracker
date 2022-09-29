using DatabaseContext.Models;
using DatabaseContext;
using Effort.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RespositoryTests.Helpers
{
    public static class TestBugHelper
    {
        public async static Task<EffortConnection> GetSeededEffortConnection()
        {
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

            return connection;
        }
    }
}
