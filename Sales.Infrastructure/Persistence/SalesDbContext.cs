using Microsoft.EntityFrameworkCore;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.DiscountAgg.Models;
using Sales.Domain.OrderAgg.Models;
using Sales.Domain.PaymentSessionAgg.Models;
using Sales.Domain.PriceHistoryAgg.Models;
using Sales.Domain.ShoppingCartAgg.Models;
using Sales.Infrastructure.Persistence.Configurations;
using Sales.Infrastructure.Persistence.Interceptors;

namespace Sales.Infrastructure.Persistence
{
    public class SalesDbContext : DbContext
    {
        private static readonly DomainEventInterceptor domainEventInterceptor;
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
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(domainEventInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
