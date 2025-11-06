using Catalog.Api.Models;
using Catalog.Api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Data
{
    public class CatalogContext : DbContext
    {

        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public CatalogContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductItem).Assembly);
            // Automatically register all Money value objects
            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();
            foreach (var entityType in entityTypes)
            {
                var clrType = entityType.ClrType;
                if (clrType == null)
                    continue;

                // Find all properties of type Money
                var moneyProperties = clrType
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(Money));

                foreach (var property in moneyProperties)
                {
                    // Configure Money as an owned type
                    modelBuilder.Entity(clrType).OwnsOne(typeof(Money), property.Name, navigationBuilder =>
                    {
                        navigationBuilder.Property<decimal>("Amount").HasColumnName($"{property.Name}_Amount");
                        navigationBuilder.Property<string>("Currency").HasColumnName($"{property.Name}_Currency");
                    });
                }
            }
            base.OnModelCreating(modelBuilder);
        }

    }
}
