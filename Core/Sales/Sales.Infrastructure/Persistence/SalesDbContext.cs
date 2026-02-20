using Common.Domain.Language.Global.ValueObjects;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.DiscountAgg.Models;
using Sales.Domain.OrderAgg.Models;
using Sales.Domain.PaymentSessionAgg.Models;
using Sales.Domain.PriceLabelAgg.Models;
using Sales.Domain.ShoppingCartAgg.Models;
using Sales.Infrastructure.Persistence.Configurations;

namespace Sales.Infrastructure.Persistence
{
    public class SalesDbContext : DbContext
    {
        private readonly EventPublisherInterceptor domainEventInterceptor;
        public SalesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<PriceLabel> ProductItemPrices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<PaymentSession> PaymentTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderEntityConfigurations).Assembly);
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(domainEventInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
