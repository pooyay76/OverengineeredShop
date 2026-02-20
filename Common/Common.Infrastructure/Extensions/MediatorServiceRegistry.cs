using Common.Application;
using Common.Application.Base;
using Common.Domain.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.Extensions
{
    public static class MediatorServiceRegistry
    {
        //No need to call this, this method is called in CommonServiceRegistry
        public static void RegisterMediator(this IServiceCollection services)
        {

            services.AddSingleton<Mediator>();
        }

        public static void RegisterMediatorHandlersFromAssembly(this IServiceCollection services,Assembly assembly)
        {
            // Get all types in the given assembly
            var handlers = assembly.GetTypes();

            // Register Command Handlers
            var commandHandlers = handlers.Where(x => !x.IsAbstract && x.BaseType is { IsGenericType: true } &&
            x.BaseType.GetGenericTypeDefinition() == typeof(CommandHandlerBase<,>)).ToList();

            foreach (var handler in commandHandlers)
            {
                var baseType = handler.BaseType;

                var requestType = baseType.GetGenericArguments()[0]; 
                var responseType = baseType.GetGenericArguments()[1]; 

                services.AddTransient(typeof(CommandHandlerBase<,>).MakeGenericType(requestType, responseType), handler);
            }

            // Register Event Handlers
            var eventHandlers = handlers.Where(x => !x.IsAbstract && x.BaseType is { IsGenericType: true } && 
            x.BaseType.GetGenericTypeDefinition() == typeof(EventHandlerBase<>)).ToList();

            foreach (var handler in eventHandlers)
            {
                var baseType = handler.BaseType;
                var eventType = baseType.GetGenericArguments()[0]; // The event type (e.g., PostCreatedEvent)


                services.AddTransient(typeof(EventHandlerBase<>).MakeGenericType(eventType), handler);
            }
        }

    }
}
