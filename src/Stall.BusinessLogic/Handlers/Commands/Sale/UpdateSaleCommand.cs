using FluentValidation;
using Stall.BusinessLogic.Extensions;
using Stall.BusinessLogic.Handlers.Commands.Base;
using Stall.BusinessLogic.Wrappers.Result;
using Stall.DataAccess.Repositories;

namespace Stall.BusinessLogic.Handlers.Commands.Sale;

public class UpdateSaleCommand : IRequestResult<int>, IUpdateCommand
{
    public UpdateSaleCommand(
        int saleId,
        int productId, 
        DateTime date,
        int count,
        decimal price)
    {
        SaleId = saleId;
        ProductId = productId;
        Date = date;
        Count = count;
        Price = price;
    }

    public int SaleId { get; set; }

    public int ProductId { get; }

    public DateTime Date { get; }

    public int Count { get; }

    public decimal Price { get; }
    
    public int UpdatedBy { get; set; }
}

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    private const int MinPositive = 0;
    
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
            .GreaterThan(MinPositive)
            .WithMessage($"SaleId have to be greater then '{MinPositive}'");
        
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

public class UpdateSaleCommandHandler : IRequestHandlerResult<UpdateSaleCommand, int>
{
    private readonly IValidator<UpdateSaleCommand> _validator;
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleCommandHandler(
        IValidator<UpdateSaleCommand> validator,
        ISaleRepository saleRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
    }
    
    public async Task<Result<int>> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
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

        var data = await _saleRepository.UpdateAsync(
            command.SaleId, 
            command.ProductId,
            command.Date,
            command.Count,
            command.Price,
            command.UpdatedBy);
        
        return Result.Success(data);
    }
}