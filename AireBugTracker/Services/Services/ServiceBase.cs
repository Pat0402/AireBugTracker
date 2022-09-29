using Repsoitories.Interfaces;
using Repsoitories.Respositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        public ServiceBase(IRepositoryBase<T> repository)
        {
            Repository = repository;
        }

        protected IRepositoryBase<T> Repository { get; set; }
        
        public Task<List<T>> GetAll() => Repository.GetAll();
    }
}
