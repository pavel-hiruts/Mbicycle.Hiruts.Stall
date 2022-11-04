using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories.Sale;

namespace Stall.BusinessLogic.Handlers.Commands.Sale;

public class DeleteSaleCommand : IRequestResult<int>
{
    public int SaleId { get; set; }
}

public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    private const int MinIdValue = 0;
    
    public DeleteSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
            .GreaterThan(MinIdValue)
            .WithMessage($"SaleId have to be greater then '{MinIdValue}'");
    }
}

public class DeleteSaleCommandHandler : IRequestHandlerResult<DeleteSaleCommand, int>
{
    private readonly ISaleRepository _saleRepository;

    private readonly IValidator<DeleteSaleCommand> _validator;

    public DeleteSaleCommandHandler(
        IValidator<DeleteSaleCommand> validator,
        ISaleRepository saleRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
    }
    
    public async Task<Result<int>> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var saleExist = await _saleRepository.ExistById(command.SaleId);
        if (!saleExist)
        {
            return Result.Fail<int>($"Could not find sale with Id = '{command.SaleId}'");
        }
        
        await _saleRepository.DeleteAsync(command.SaleId);
        
        return Result.Success(command.SaleId);
    }
}