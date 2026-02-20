
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Common.Domain.Language.Warehouse.ValueObjects;
using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public class InventoryLogEntityConfigurations : IEntityTypeConfiguration<InventoryLog>
    {
        public void Configure(EntityTypeBuilder<InventoryLog> builder)
        {
            builder.ToTable("Inventory Logs");
            builder.HasKey(i => i.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new InventoryLogId(x));
            builder.Property(x => x.BillId).HasConversion(x => x.Value, x => new BillId(x));
            builder.Property(x => x.ProductItemId).HasConversion(x => x.Value, x => new ProductItemId(x));
            builder.Property(x=>x.OperatorId).HasConversion(x=>x.Value,x=>new UserId(x));
            builder.Property(x=>x.CustomerId).HasConversion(x=>x.Value,x=>new UserId(x));
            builder.Property(x=>x.OperatorName).HasMaxLength(80);
            builder.Property(x=>x.Description).HasMaxLength(60);
        }
    }
}
