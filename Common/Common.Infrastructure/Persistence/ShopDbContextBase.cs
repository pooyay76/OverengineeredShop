using Common.Domain.Models;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence
{
    public abstract class ShopDbContextBase : DbContext
    {

        private readonly EventPublisherInterceptor _interceptor;

        public ShopDbContextBase(DbContextOptions options,EventPublisherInterceptor interceptor) : base(options)
        {
            _interceptor = interceptor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddEventPublisherInterceptor(_interceptor);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureFrameworkEntities();
            base.OnModelCreating(modelBuilder);
        }
    }
}
