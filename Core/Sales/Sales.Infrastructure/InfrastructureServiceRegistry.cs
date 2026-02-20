using Common.Application.Contracts;
using Common.Domain.Language.Catalog.Events.Global;
using Common.Infrastructure;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application;
using Sales.Application.ShoppingCartUseCases.Commands;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.Contracts;
using Sales.Domain.DiscountAgg.Contracts;
using Sales.Domain.OrderAgg.Contracts;
using Sales.Domain.PriceLabelAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Infrastructure.External.Client.Catalog;
using Sales.Infrastructure.External.Client.ZarinPal;
using Sales.Infrastructure.Options;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Persistence.Repositories;

namespace Sales.Infrastructure
{
    public static class InfrastructureServiceRegistry
    {

        public static void RegisterInfrastuctureServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            serviceCollection.AddDbContext<SalesDbContext>(x => x.UseNpgsql(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            CommonServiceRegistry.RegisterCommonServices(serviceCollection, connectionString);

            serviceCollection.AddOptions();
            serviceCollection.Configure<ZarinPalOptions>(configuration.GetSection("ZarinPal"));
            serviceCollection.AddTransient<IPaymentGatewayService, ZarinPalService>();
            serviceCollection.AddScoped<IDiscountRepository,DiscountRepository>();
            serviceCollection.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            serviceCollection.AddScoped<IBillRepository, BillRepository>();
            serviceCollection.RegisterApplicationServices(configuration);
            serviceCollection.AddScoped<IFrameworkUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICatalogClient, CatalogClient>();

            serviceCollection.AddSingleton<KafkaEventConsumerService<NewPriceAddedEvent>>();
            serviceCollection.AddHostedService<KafkaEventConsumerBackgroundService<NewPriceAddedEvent>>();

        }


    }
}
