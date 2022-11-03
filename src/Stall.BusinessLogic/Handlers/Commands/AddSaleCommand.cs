using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Commands
{
    public class AddSaleCommand : IRequestResult<int>
    {
        public AddSaleCommand(
            int productId, 
            DateTime date,
            int count,
            decimal price)
        {
            ProductId = productId;
            Date = date;
            Count = count;
            Price = price;
        }

        public int ProductId { get; }

        public DateTime Date { get; }

        public int Count { get; }

        public decimal Price { get; }
    }

    public class AddSaleCommandHandler : IRequestHandlerResult<AddSaleCommand, int>
    {
        private readonly IProductRepository _productRepository;

        private readonly ISaleRepository _saleRepository;

        public AddSaleCommandHandler(
            IProductRepository productRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository)); ;
        }

        public async Task<Result<int>> Handle(AddSaleCommand command, CancellationToken cancellationToken)
        {
            var productExist = await _productRepository.ExistById(command.ProductId);
            
            if (!productExist)
            {
                return Result.Fail<int>($"Could not find product with Id = '{command.ProductId}'");
            }

            var data = await _saleRepository
                .AddAsync(
                command.ProductId, 
                command.Date, 
                command.Count, 
                command.Price);

            return Result.Success(data);
        }
    }
}
