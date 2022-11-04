using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Commands.Product;

public class UpdateProductCommand : IRequestResult<int>
{
    public int  ProductId { get; set; }
    public string ProductName { get; set; }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private const int MinIdValue = 0;
    private const int ProductNameMaxLength = 50;
    
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(MinIdValue)
            .WithMessage($"ProductId have to be greater then '{MinIdValue}'");
        
        RuleFor(x => x.ProductName)
            .MaximumLength(ProductNameMaxLength)
            .WithMessage(x=> $"ProductName length have to be less then '{ProductNameMaxLength}', actual length is '{x.ProductName.Length}'");
    }
}

public class UpdateProductCommandHandler : IRequestHandlerResult<UpdateProductCommand, int>
{
    private readonly IValidator<UpdateProductCommand> _validator;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(
        IValidator<UpdateProductCommand> validator,
        IProductRepository productRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
    
    public async Task<Result<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
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

        var data = await _productRepository.UpdateAsync(
            command.ProductId, 
            command.ProductName);
        
        return Result.Success(data);
    }
}