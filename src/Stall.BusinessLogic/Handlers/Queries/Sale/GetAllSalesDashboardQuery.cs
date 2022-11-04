using MediatR;
using Stall.BusinessLogic.Dtos;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories.Sale;

namespace Stall.BusinessLogic.Handlers.Queries.Sale
{
    public class GetAllSalesDashboardQuery : IRequestResult<IEnumerable<SalesDashboardDto>>
    {
    }

    public class GetAllSalesForDashboardQueryHandler : IRequestHandlerResult<GetAllSalesDashboardQuery, IEnumerable<SalesDashboardDto>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesForDashboardQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        async Task<Result<IEnumerable<SalesDashboardDto>>> IRequestHandler<GetAllSalesDashboardQuery, Result<IEnumerable<SalesDashboardDto>>>.Handle(GetAllSalesDashboardQuery request, CancellationToken cancellationToken)
        {
            var data = (await _saleRepository.GetAllSalesDashboardAsync())
                .Select(sale => new SalesDashboardDto
                {
                    Id = sale.Id,
                    Date = sale.Date,
                    ProductName = sale.Product.Name,
                    Count = sale.Count,
                    Price = sale.Price,
                });

            return Result.Success(data);
        }
    }
}
