using DatabaseContext.Models;
using Moq;
using Repsoitories.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    public class UserServiceTests
    {
        [Test]
        public async Task CreateUser_CreatesUser()
        {
            // Create mock repository
            var userRepositoryMock = new Mock<IUserRepository>();
            var theUser = new User()
            {
                Name = "Arthur McArthur"
            };

            userRepositoryMock.Setup(b => b.CreateAsync(theUser)).Returns(Task.FromResult<User>(theUser));

            // Create the service
            var bugService = new UserService(userRepositoryMock.Object);

            // Call action
            var result = await bugService.CreateAsync(theUser);

            // Verify
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.Target.Name, Is.EqualTo(theUser.Name));
            });

            userRepositoryMock.Verify(b => b.CreateAsync(theUser), Times.Once());
        }
    }
}
