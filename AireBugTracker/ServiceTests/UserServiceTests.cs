using DatabaseContext.Models;
using Moq;
using Repositories.Interfaces;
using Services.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    public class UserServiceTests
    {
        [Test]
        public async Task GetAll_ReturnsAllUsers()
        {
            // Create mock repository
            var userRepositoryMock = new Mock<IUserRepository>();
            var theUsers = new List<User> {
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
            };

            userRepositoryMock.Setup(b => b.GetAllOrdered()).Returns(Task.FromResult(theUsers));

            // Create the service
            var userService = new UserService(userRepositoryMock.Object);

            // Call action
            var result = await userService.GetAllAsync();

            // Verify
            Assert.Multiple(() =>
            {
                Assert.That(result?.Target, Is.Not.Null);
                Assert.That(result?.Target, Has.Count.EqualTo(3));
                Assert.That(result?.Status, Is.EqualTo(HttpStatusCode.OK));
            });

            var theUser = theUsers.Single(u => u.Id == 2);
            Assert.That(theUser.Name, Is.EqualTo("Larry McLarry"));

            userRepositoryMock.Verify(u => u.GetAllOrdered(), Times.Once());
        }

        [Test]
        public async Task CreateUser_CreatesUser()
        {
            // Create mock repository
            var userRepositoryMock = new Mock<IUserRepository>();
            var theUser = new User()
            {
                Name = "Arthur McArthur"
            };

            userRepositoryMock.Setup(u => u.CreateAsync(It.IsAny<User>())).Returns(Task.FromResult<User>(theUser));

            // Create the service
            var userService = new UserService(userRepositoryMock.Object);

            // Call action
            var newUser = new UserDTO
            {
                Name = theUser.Name
            };
            var result = await userService.CreateAsync(newUser);

            // Verify
            Assert.Multiple(() =>
            {
                Assert.That(result.Status, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(result.Target?.Name, Is.EqualTo(theUser.Name));
            });

            userRepositoryMock.Verify(u => u.CreateAsync(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public async Task UpdateUser_UpdatesUser()
        {
            // Create mock repository
            var userRepositoryMock = new Mock<IUserRepository>();
            var theUserDTO = new UserDTO()
            {
                Name = "Sally McBarry"
            };

            var theUserEntity = new User
            {
                Id = 1,
                Name = "Sally McBarry"
            };

            userRepositoryMock.Setup(u => u.UpdateAsync(It.IsAny<User>())).Returns(Task.FromResult<User>(theUserEntity));

            // Create the service
            var userService = new UserService(userRepositoryMock.Object);

            // Call action
            var result = await userService.UpdateAsync(1, theUserDTO);

            // Verify
            Assert.Multiple(() =>
            {
                Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.Target?.Name, Is.EqualTo(theUserEntity.Name));
            });

            userRepositoryMock.Verify(u => u.UpdateAsync(It.IsAny<User>()), Times.Once());
        }
    }
}
