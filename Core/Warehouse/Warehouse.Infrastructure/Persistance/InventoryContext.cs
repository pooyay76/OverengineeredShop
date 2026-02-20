using Inventory.Domain.Models;
using Inventory.Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistance
{
    public class InventoryContext : DbContext
    {
        public DbSet<ProductItemInventory> ProductItemInventories { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public InventoryContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductItemInventoryEntityConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
