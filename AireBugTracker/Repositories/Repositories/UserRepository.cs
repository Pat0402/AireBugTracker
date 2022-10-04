using DatabaseContext;
using DatabaseContext.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Respositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BugTrackerContext context) : base(context)
        {
        }

        public override Task<List<User>> GetAllOrdered()
        {
            return DbContext.Users.OrderBy(u => u.Name).ToListAsync();
        }

        public override Task<List<Bug>> GetFiltered()
        {
            throw new NotImplementedException();
        }

        public async override Task<User> UpdateAsync(User entity)
        {
            var theUser = await DbContext.Users.SingleAsync(b => b.Id == entity.Id);
            theUser.Name = entity.Name;

            await DbContext.SaveChangesAsync();

            return theUser;
        }
    }
}
