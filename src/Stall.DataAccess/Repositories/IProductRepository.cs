using Stall.DataAccess.Model;

namespace Stall.DataAccess.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ExistById(int id);
}