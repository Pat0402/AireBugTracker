using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IServiceBase <T, K> where T : class where K : class
    {
        Task<ServiceResult<List<T>>> GetAllAsync();
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<T>> CreateAsync(K dto);
        Task<ServiceResult<T>> CreateAsync(T entity);
        Task<ServiceResult<T>> UpdateAsync(int id, K dto);
    }
}
