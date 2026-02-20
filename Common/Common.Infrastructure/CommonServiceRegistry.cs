using Common.Application;
using Common.Application.Contracts;
using Common.Domain.Contracts;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.MessageBroker;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Interceptors;
using Common.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure
{
    public class CommonServiceRegistry
    {
        public static void RegisterCommonServices(IServiceCollection services,string connectionString)
        {

            services.RegisterMediator();

            services.AddDbContext<FrameworkDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<EventPublisherInterceptor>();
            services.AddScoped<IFrameworkUnitOfWork, FrameworkUnitOfWork>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<IEventProducerService, KafkaProducer>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<EventPublisher>();
            services.AddHostedService<OutboxBackgroundService>();
            services.RegisterMediatorHandlersFromAssembly(Assembly.GetCallingAssembly());

        }
    }
}
