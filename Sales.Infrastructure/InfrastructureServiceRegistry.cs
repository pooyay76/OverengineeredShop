using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Application;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.OrderAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Contracts;
using Sales.Infrastructure.Configurations;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Persistence.Repositories;

namespace Sales.Infrastructure
{
    public static class InfrastructureServiceRegistry
    {

        public static void RegisterInfrastuctureServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {

            serviceCollection.AddOptions();
            serviceCollection.Configure<ZarinPalOptions>(configuration.GetSection(""));
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            serviceCollection.AddScoped<IBillRepository, BillRepository>();
            RegisterSqlService(serviceCollection, configuration);
            serviceCollection.RegisterApplicationServices(configuration);
        }

        private static void RegisterSqlService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sql");
            serviceCollection.AddDbContext<SalesDbContext>(x => x.UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}
