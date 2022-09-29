using Repsoitories.Interfaces;
using Repsoitories.Respositories;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        public ServiceBase(IRepositoryBase<T> repository)
        {
            Repository = repository;
        }

        protected IRepositoryBase<T> Repository { get; set; }

        public Task<List<T>> GetAll() => Repository.GetAll();
        public Task<T> GetById(int id) => Repository.GetById(id);
        public async Task<ServiceResult<T>> CreateAsync(T entity)
        {
            try
            {
                var persistedEntity = await Repository.CreateAsync(entity);

                return new ServiceResult<T>
                {
                    Target = persistedEntity,
                    IsSuccessful = true,
                    Message = $"{nameof(T)} successfully created"
                };
            }
            catch (DbUpdateException)
            {
                return new ServiceResult<T>
                {
                    IsSuccessful = false,
                    Message = $"Failed to create {nameof(T)}, there was a conflict when writing to the database"
                };
            }
        }

        public abstract Task<ServiceResult<T>> UpdateAsync(T entity);
    }
}
