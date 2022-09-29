using DatabaseContext.Models;
using DatabaseContext;
using Effort.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryTests.Helpers
{
    public static class TestUserHelper
    {
        public async static Task<EffortConnection> GetSeededEffortConnection()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            using (var dbContext = new BugTrackerContext(connection))
            {
                dbContext.Users.AddRange(new List<User> {
                    new User
                    {
                        Id = 1,
                        Name = "Barry McBarry"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Larry McLarry"
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Sally McSally"
                    }
                });

                await dbContext.SaveChangesAsync();
            }

            return connection;
        }
    }
}
