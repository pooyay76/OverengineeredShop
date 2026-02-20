using Common.Application;
using Common.Domain.Base;
using Common.Domain.Language.Global.ValueObjects;
using Common.Infrastructure.Persistence.Configurations;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Extensions
{
    public static class DbContextConfigurationsExtension
    {
        public static void ConfigureFrameworkEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventEntityTypeConfigurations).Assembly);
        }
        public static void AddEventPublisherInterceptor(this DbContextOptionsBuilder dbContextOptions,EventPublisherInterceptor interceptor)
        {
            dbContextOptions.AddInterceptors(interceptor);
        }
    }
}
