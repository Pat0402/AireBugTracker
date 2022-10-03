using DatabaseContext.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : ServiceBase<User, UserDTO>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public override async Task<ServiceResult<User>> CreateAsync(UserDTO dto)
        {
            var theUser = new User
            {
                Name = dto.Name
            };

            return await CreateAsync(theUser);
        }

        public async override Task<ServiceResult<User>> UpdateAsync(int id, UserDTO entity)
        {
            try
            {
                var theUser = new User
                {
                    Id = id,
                    Name = entity.Name,
                };
                var updatedUser = await Repository.UpdateAsync(theUser);

                return new ServiceResult<User>
                {
                    Status = HttpStatusCode.OK,
                    Target = updatedUser,
                    Message = $"User: \"{id}\" successfully updated"
                };
            }
            catch (DbUpdateException)
            {
                // Should be impossible to reach with current requirments, "IsOpen" is the only field a user can edit
                return new ServiceResult<User>
                {
                    Status = HttpStatusCode.Conflict,
                    Message = $"Failed to update user: \"{id}\", there was a conflict in the database"
                };
            }
        }
    }
}
