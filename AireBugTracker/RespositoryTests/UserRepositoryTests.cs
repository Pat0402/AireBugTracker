using DatabaseContext.Models;
using DatabaseContext;
using Repsoitories.Respositories;
using RespositoryTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryTests
{
    public class UserRepositoryTests
    {
        [Test]
        public async Task CreateUser_CreatesUser()
        {
            var connection = await TestBugHelper.GetSeededEffortConnection();
            var newUser = new User { 
                Name = "Nelly McNelly"
            };

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Persist the new user
                var userRepository = new UserRepository(dbContext);
                await userRepository.CreateAsync(newUser);

                // Retrieve the new user for the DB
                var retrievedBug = await userRepository.GetById(newUser.Id);

                // Verify
                Assert.That(retrievedBug.Name, Is.EqualTo(newUser.Name));
            }
        }
    }
}
