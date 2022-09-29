﻿using DatabaseContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsoitories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> CreateAsync(T entity);
    }
}