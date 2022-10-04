using Repositories.Interfaces;
using Repositories.Respositories;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public abstract class ServiceBase<T, K> : IServiceBase<T, K> where T : class where K :class
    {
        public ServiceBase(IRepositoryBase<T> repository)
        {
            Repository = repository;
        }

        protected IRepositoryBase<T> Repository { get; set; }

        public async Task<ServiceResult<List<T>>> GetAllAsync()
        {
            var theBugs = await Repository.GetAllOrdered();

            return new ServiceResult<List<T>>
            {
                Status = HttpStatusCode.OK,
                Target = theBugs,
                Message = "Retrieved all bugs"
            };
        }

        public async Task<ServiceResult<T>> GetByIdAsync(int id)
        {
            var theBug = await Repository.GetById(id);

            if (theBug == null)
            {
                return new ServiceResult<T>
                {
                    Status = HttpStatusCode.NotFound,
                    Message = $"Resource with Id: \"{id}\" not found"
                };
            }

            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Target = theBug,
                Message = "Retrieved all bugs"
            };
        }

        protected async Task<ServiceResult<T>> CreateAsync(T entity)
        {
            try
            {
                var persistedEntity = await Repository.CreateAsync(entity);

                return new ServiceResult<T>
                {
                    Status= HttpStatusCode.Created,
                    Target = persistedEntity,
                    Message = $"{nameof(T)} successfully created"
                };
            }
            catch (DbUpdateException)
            {
                return new ServiceResult<T>
                {
                    Status = HttpStatusCode.Conflict,
                    Message = $"Failed to create {nameof(T)}, there was a conflict when writing to the database"
                };
            }
        }

        public abstract Task<ServiceResult<T>> CreateAsync(K dto);

        public abstract Task<ServiceResult<T>> UpdateAsync(int id, K dto);
    }
}
