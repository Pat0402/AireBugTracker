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
        public async Task GetAll_ReturnsAllUsers()
        {
            // Set up in memory database
            var connection = await TestUserHelper.GetSeededEffortConnection();

            using (var dbContext = new BugTrackerContext(connection))
            {
                // Retrieve the persisted data
                var userRepository = new UserRepository(dbContext);
                var theUsers = await userRepository.GetAll();

                // Verify
                Assert.That(theUsers, Has.Count.EqualTo(3));

                var firstUser = theUsers.Single(bug => bug.Id == 1);

                Assert.That(firstUser.Name, Is.EqualTo("Barry McBarry"));
            }
        }

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
