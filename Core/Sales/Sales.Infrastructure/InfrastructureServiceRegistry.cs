using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application;
using Sales.Application.Contracts;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.DiscountAgg.Contracts;
using Sales.Domain.External;
using Sales.Domain.OrderAgg.Contracts;
using Sales.Domain.PriceLabelAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Infrastructure.Configurations;
using Sales.Infrastructure.External.Client.Catalog;
using Sales.Infrastructure.External.Client.ZarinPal;
using Sales.Infrastructure.External.MessageBroker;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Persistence.Repositories;

namespace Sales.Infrastructure
{
    public static class InfrastructureServiceRegistry
    {

        public static void RegisterInfrastuctureServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {

            RegisterSqlService(serviceCollection, configuration);
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
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICatalogClient, CatalogClient>();
            serviceCollection.AddSingleton<KafkaConsumerService>();
        }

        private static void RegisterSqlService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");
            serviceCollection.AddDbContext<SalesDbContext>(x => x.UseNpgsql(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}
