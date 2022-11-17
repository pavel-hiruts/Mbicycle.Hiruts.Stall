using MediatR;
using Stall.BusinessLogic.Handlers.Commands.Base;

namespace Stall.WebApi;

public class MediatorCommandsPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        switch (request)
        {
            case ICreateCommand createCommand:
                createCommand.CreatedBy = 1;//todo get user id from http context 
                break;
            case IUpdateCommand updateCommand:
                updateCommand.UpdatedBy = 1;
                break;
        }

        var result = await next();
        
        return result;
    }
}