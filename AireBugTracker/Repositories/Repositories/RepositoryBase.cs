using DatabaseContext;
using DatabaseContext.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public Task<List<T>> GetAllOrdered(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().OrderBy(predicate).ToListAsync();
        }

        public abstract Task<List<T>> GetAllOrdered();

        public abstract Task<List<Bug>> GetFiltered();
    }
}
