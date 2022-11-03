using MediatR;

namespace Stall.BusinessLogic.Wrappers.Result;

public interface IRequestResult<TOut> : IRequest<Result<TOut>> { }