using DatabaseContext.Models;
using Repsoitories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
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

        public override Task<ServiceResult<User>> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
