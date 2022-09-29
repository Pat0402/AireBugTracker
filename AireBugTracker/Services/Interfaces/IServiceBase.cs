using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceBase <T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<ServiceResult<T>> CreateAsync(T entity);
        Task<ServiceResult<T>> UpdateAsync(T entity);
    }
}
