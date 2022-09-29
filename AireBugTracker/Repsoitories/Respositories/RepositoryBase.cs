using DatabaseContext;
using Repsoitories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsoitories.Respositories
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
    }
}
