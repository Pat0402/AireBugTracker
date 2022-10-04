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
    public class BugRepository : RepositoryBase<Bug>, IBugRepository
    {
        public BugRepository(BugTrackerContext context) : base(context)
        {
        }

        public override Task<List<Bug>> GetAllOrdered()
        {
            return DbContext.Bugs.OrderBy(b => b.OpenedDate).ToListAsync();
        }

        public override Task<List<Bug>> GetFiltered()
        {
            return DbContext.Bugs
                .Where(b => b.IsOpen)
                .OrderBy(b => b.OpenedDate).ToListAsync();
        }

        public async override Task<Bug> UpdateAsync(Bug entity)
        {
            var theBug = await DbContext.Bugs.SingleAsync(b => b.Id == entity.Id);
            theBug.Details = entity.Details;
            theBug.IsOpen = entity.IsOpen;
            theBug.Title = entity.Title;
            theBug.UserId = entity.UserId;

            await DbContext.SaveChangesAsync();

            return theBug;
        }
    }
}
