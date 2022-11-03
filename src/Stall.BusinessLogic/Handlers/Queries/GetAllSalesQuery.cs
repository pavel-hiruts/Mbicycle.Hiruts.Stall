﻿using MediatR;
using Stall.BusinessLogic.Dtos;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Queries
{
    public class GetAllSalesQuery : IRequestResult<IEnumerable<SaleDto>>
    {
    }

    public class GetAllSalesQueryHandler : IRequestHandlerResult<GetAllSalesQuery, IEnumerable<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetAllSalesQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        async Task<Result<IEnumerable<SaleDto>>> IRequestHandler<GetAllSalesQuery, Result<IEnumerable<SaleDto>>>.Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var result = (await _saleRepository.GetAsync())
                .Select(sale =>
                    new SaleDto
                    {
                        Id = sale.Id,
                        Date = sale.Date,
                        ProductName = sale.Product.Name,
                        Count = sale.Count,
                        Price = sale.Price,
                    });

            return Result.Success(result);
        }
    }
}