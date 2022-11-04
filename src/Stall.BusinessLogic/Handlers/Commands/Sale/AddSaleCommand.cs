using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Commands.Sale;

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
    
public class AddSaleCommandValidator : AbstractValidator<AddSaleCommand>
{
    private const int MinPositive = 0;
    
    public AddSaleCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(MinPositive)
            .WithMessage($"ProductId have to be greater then '{MinPositive}'");
            
        RuleFor(x => x.Count)
            .GreaterThan(MinPositive)
            .WithMessage($"Count have to be greater then '{MinPositive}'");
            
        RuleFor(x => x.Price)
            .GreaterThan(MinPositive)
            .WithMessage($"Price have to be greater then '{MinPositive}'");

        RuleFor(x => x.Date)
            .NotNull()
            .WithMessage("Date have to not be empty");
    }
}    

public class AddSaleCommandHandler : IRequestHandlerResult<AddSaleCommand, int>
{
    private readonly IValidator<AddSaleCommand> _validator;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;

    public AddSaleCommandHandler(
        IValidator<AddSaleCommand> validator,
        IProductRepository productRepository,
        ISaleRepository saleRepository)
    {
        _validator = validator;
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository)); ;
    }

    public async Task<Result<int>> Handle(AddSaleCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }

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