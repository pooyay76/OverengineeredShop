
using Common.Application.Base;
using Common.Domain.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Application
{
    public class Mediator
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Mediator> _logger;
        public Mediator(IServiceScopeFactory serviceScopeFactory,ILogger<Mediator> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<TResponse> SendAsync<TResponse>(CommandBase request)
        {
            var requestType = request.GetType(); // e.g. PostCreatedEvent
            object handler;
            // Build IEventHandler<eventType>
            var handlerType = typeof(CommandHandlerBase<,>).MakeGenericType(requestType,typeof(TResponse));

            //TODO: test the perfomance of this code later
            using var scope = _serviceScopeFactory.CreateScope();
            try
            {    
                handler = scope.ServiceProvider.GetRequiredService(handlerType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Handler for command:{handlerType.Name} not found in DI container.", ex);
            }
            try
            {
            var method = handlerType.GetMethod("HandleAsync");

            return await (Task<TResponse>)method.Invoke(handler, [request]);
            }
            catch
            {
                throw new InvalidOperationException($"Method with input type:{request.GetType().Name} and " +
                    $"output type:{typeof(TResponse).Name} was not found on command handler");
            }
        }

        public async Task PublishAsync(EventBase @event)
        {
            var requestType = @event.GetType(); // e.g. PostCreatedEvent
            List<object> handlers;


            // Build IEventHandler<eventType>
            var handlerType = typeof(EventHandlerBase<>).MakeGenericType(requestType);

            using var scope = _serviceScopeFactory.CreateScope();


            try
            {
                handlers = scope.ServiceProvider.GetServices(handlerType).ToList() ?? new();
            }
            catch
            {
                handlers = [];
                _logger.LogCritical($"No handlers were found for event type: {@event.GetType()}");
            }
            foreach (var handler in handlers)
            {
                try
                {
                    var method = handlerType.GetMethod("HandleAsync");

                    await (Task)method.Invoke(handler, [@event]);
                }
                catch
                {
                    _logger.LogError($"No method named HandleAsync were found in event handler: {handler.GetType()}");
                }
            }
        }
    }
}
