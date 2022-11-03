using Stall.DataAccess.Model;

namespace Stall.DataAccess.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    Task<ICollection<Sale>> GetAllSalesDashboardAsync();

    Task<int> AddAsync(
        int productId, 
        DateTime date, 
        int count, 
        decimal price);
}