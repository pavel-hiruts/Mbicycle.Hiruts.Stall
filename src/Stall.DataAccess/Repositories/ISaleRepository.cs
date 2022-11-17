using Stall.DataAccess.Model.Domain;
using Stall.DataAccess.Repositories.Base;

namespace Stall.DataAccess.Repositories;

public interface ISaleRepository : IRepository<Sale>
{
    Task<ICollection<Sale>> GetAllSalesDashboardAsync();

    Task<int> AddAsync(int productId,
        DateTime date,
        int count,
        decimal price, 
        int createdBy);

    Task<bool> ExistById(int id);
    Task<int> UpdateAsync(int saleId,
        int productId,
        DateTime date,
        int count,
        decimal price, 
        int updatedBy);
}