using DatabaseContext;
using DatabaseContext.Models;
using Repsoitories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsoitories.Respositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BugTrackerContext context) : base(context)
        {
        }

        public override Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
