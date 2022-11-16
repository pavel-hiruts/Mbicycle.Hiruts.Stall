using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories;

public interface IProductRepository : IRepository<Model.Product>
{
    Task<int> AddAsync(string name);
    
    Task<bool> ExistById(int id);
    
    Task<bool> ExistByName(string name);
    
    Task<int> UpdateAsync(int id, string name);
}