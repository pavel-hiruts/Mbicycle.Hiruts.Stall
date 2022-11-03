using MediatR;

namespace Stall.BusinessLogic.Wrappers.Result;

public interface IRequestHandlerResult<TIn, TOut> : IRequestHandler<TIn, Result<TOut>> where TIn : IRequest<Result<TOut>> { }
