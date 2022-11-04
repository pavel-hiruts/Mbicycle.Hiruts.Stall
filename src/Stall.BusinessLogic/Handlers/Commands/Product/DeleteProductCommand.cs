using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories.Product;

namespace Stall.BusinessLogic.Handlers.Commands.Product;

public class DeleteProductCommand : IRequestResult<int>
{
    public int ProductId { get; set; }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private const int MinIdValue = 0;
    
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(MinIdValue)
            .WithMessage($"ProductId have to be greater then '{MinIdValue}'");
    }
}

public class DeleteProductCommandHandler : IRequestHandlerResult<DeleteProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    private readonly IValidator<DeleteProductCommand> _validator;

    public DeleteProductCommandHandler(
        IValidator<DeleteProductCommand> validator,
        IProductRepository productRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
    
    public async Task<Result<int>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
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
        
        await _productRepository.DeleteAsync(command.ProductId);
        
        return Result.Success(command.ProductId);
    }
}