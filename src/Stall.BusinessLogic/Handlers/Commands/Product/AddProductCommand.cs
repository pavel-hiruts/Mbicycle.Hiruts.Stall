using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Commands.Product;

public class AddProductCommand : IRequestResult<int>
{
    public string ProductName { get; set; }
}

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    private const int ProductNameMaxLength = 50;
    
    public AddProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .MaximumLength(ProductNameMaxLength)
            .WithMessage(x=> $"ProductName length have to be less then '{ProductNameMaxLength}', actual length is '{x.ProductName.Length}'");
    }
}

public class AddProductCommandHandler : IRequestHandlerResult<AddProductCommand, int>
{
    private readonly IValidator<AddProductCommand> _validator;
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(
        IValidator<AddProductCommand> validator,
        IProductRepository productRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
    
    public async Task<Result<int>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var exist = await _productRepository.ExistByName(command.ProductName);
        if (exist)
        {
            return Result.Fail<int>($@"Could not add product with name '{command.ProductName}' which is already exists");
        }

        var data = await _productRepository.AddAsync(command.ProductName);
        
        return Result.Success(data);
    }
}