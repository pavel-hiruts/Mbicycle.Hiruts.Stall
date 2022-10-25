using Stall.BusinessLogic.Dtos;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic;

public class AllSalesService : IAllSalesService
{
    private readonly ISaleRepository _saleRepository;

    public AllSalesService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
    }

    public IEnumerable<SaleDto> GetAllSales()
    {
        var result = _saleRepository.Get().Select(sale =>
            new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                ProductName = sale.Product.Name,
                Count = sale.Count,
                Price = sale.Price,
            });

        return result;
    }
}