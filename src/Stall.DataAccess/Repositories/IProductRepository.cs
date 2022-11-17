using Stall.DataAccess.Model.Domain;
using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<int> AddAsync(string name, int createdBy);
    
    Task<bool> ExistById(int id);
    
    Task<bool> ExistByName(string name);
    
    Task<int> UpdateAsync(int id, string name, int updatedBy);
}