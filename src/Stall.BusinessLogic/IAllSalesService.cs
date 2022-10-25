using Stall.BusinessLogic.Dtos;

namespace Stall.BusinessLogic;

internal interface IAllSalesService
{
    IEnumerable<SaleDto> GetAllSales();
}