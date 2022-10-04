using DatabaseContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllOrdered();
        Task<List<Bug>> GetFiltered();
        Task<T> GetById(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
