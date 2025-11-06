using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Domain.BillAgg;
using Sales.Domain.OrderAgg;
using Sales.Domain.ShoppingCartAgg;

namespace Sales.Application
{
    public static class ApplicationServiceRegistry
    {
        public static void RegisterApplicationServices(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddTransient<ShoppingCartManager>();
            serviceCollection.AddTransient<BillManager>();
            serviceCollection.AddTransient<OrderManager>();
        }



    }
}
