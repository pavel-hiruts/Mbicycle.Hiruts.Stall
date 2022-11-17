namespace Stall.BusinessLogic.Handlers.Commands.Base;

public interface IUpdateCommand
{
    public int UpdatedBy { get; set; }
}