using MediatR;
using Stall.BusinessLogic.Wrappers;
using Stall.DataAccess.Model;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Commands
{
    public class AddSaleCommand : IRequest<Result<int>>
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

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }

    public class AddSaleCommandHandler : IRequestHandler<AddSaleCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;

        private readonly ISaleRepository _saleRepository;

        public AddSaleCommandHandler(
            IProductRepository productRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); ;
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository)); ;
        }

        public Task<Result<int>> Handle(AddSaleCommand command, CancellationToken cancellationToken)
        {
            var product = _productRepository.Get(command.ProductId);

            if (product.Id != command.ProductId)
            {
                return Result.FailAsync<int>($"Could not find product with Id = '{command.ProductId}'");
            }

            var newSale = new Sale()
            {
                Product = product,
                Date = command.Date,
                Price = command.Price,
                Count = command.Count,
            };

            var result = _saleRepository.Add(newSale);

            return Result.SuccessAsync(result.Id);
        }
    }
}
