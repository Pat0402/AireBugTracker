using DatabaseContext.Models;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public async override Task<ServiceResult<User>> UpdateAsync(User entity)
        {
            try
            {
                var updatedEntity = await Repository.UpdateAsync(entity);

                return new ServiceResult<User>
                {
                    Target = updatedEntity,
                    IsSuccessful = true,
                    Message = $"User: \"{entity.Id}\" successfully updated"
                };
            }
            catch (DbUpdateException)
            {
                // Should be impossible to reach with current requirments, "IsOpen" is the only field a user can edit
                return new ServiceResult<User>
                {
                    IsSuccessful = false,
                    Message = $"Failed to update user: \"{entity.Id}\", there was a conflict in the database"
                };
            }
        }
    }
}
