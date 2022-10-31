using Stall.BusinessLogic.Dtos;

namespace Stall.BusinessLogic;

public interface ISalesService
{
    IEnumerable<SaleDto> GetAllSales();
}