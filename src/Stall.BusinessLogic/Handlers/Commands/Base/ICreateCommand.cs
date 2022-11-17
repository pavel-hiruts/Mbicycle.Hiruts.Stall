namespace Stall.BusinessLogic.Handlers.Commands.Base;

public interface ICreateCommand
{
    public int CreatedBy { get; set; }
}