﻿using Stall.DataAccess.Model.Domain.Base;

namespace Stall.DataAccess.Repositories.Base;

public interface IRepository<T> where T : Entity
{
    Task<T> AddAsync(T item);

    Task DeleteAsync (T item);

    Task DeleteAsync(int id);

    Task UpdateAsync(T item);

    Task<ICollection<T>> GetAsync();

    Task<T> GetAsync(int id);
}