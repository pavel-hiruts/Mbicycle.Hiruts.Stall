using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories.Sale;

public interface ISaleRepository : IRepository<Model.Sale>
{
    Task<ICollection<Model.Sale>> GetAllSalesDashboardAsync();

    Task<int> AddAsync(
        int productId, 
        DateTime date, 
        int count, 
        decimal price);

    Task<bool> ExistById(int id);
    Task<int> UpdateAsync(
        int saleId,
        int productId, 
        DateTime date, 
        int count, 
        decimal price);
}