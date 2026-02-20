namespace Common.Application.Base
{
    public abstract class CommandHandlerBase<TRequest,TResponse> 
        where TRequest : CommandBase 
    {
        public abstract Task<TResponse> HandleAsync(TRequest request);

    }

}
