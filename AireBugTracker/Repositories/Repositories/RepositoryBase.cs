using DatabaseContext;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Respositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase(BugTrackerContext context)
        {
            DbContext = context;
        }

        protected BugTrackerContext DbContext { get; set; }

        public Task<List<T>> GetAll() => DbContext.Set<T>().ToListAsync();
        public Task<T> GetById(int id) => DbContext.Set<T>().FindAsync(id);
        public async Task<T> CreateAsync(T entity)
        {
            var result = DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            
            return result;
        }

        public abstract Task<T> UpdateAsync(T entity);
    }
}
