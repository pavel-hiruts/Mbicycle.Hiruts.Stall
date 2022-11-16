using Stall.BusinessLogic.Handlers.Queries.Dtos;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Queries.Product;

public class GetAllProductsQuery : IRequestResult<IEnumerable<ProductDto>>
{
}

public class GetAllProductsQueryHandler : IRequestHandlerResult<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var data = (await _productRepository.GetAsync())
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
            });

        return Result.Success(data);
    }
}