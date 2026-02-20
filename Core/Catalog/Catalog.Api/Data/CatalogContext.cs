using Catalog.Api.Data.Configurations;
using Catalog.Api.Models;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Data
{
    public class CatalogContext : ShopDbContextBase
    {

        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public CatalogContext(DbContextOptions options,EventPublisherInterceptor interceptor) : base(options,interceptor)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductItemTypeConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
