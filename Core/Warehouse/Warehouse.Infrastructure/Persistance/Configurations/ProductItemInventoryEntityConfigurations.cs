using Common.Domain.Language.Catalog.ValueObjects;
using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public class ProductItemInventoryEntityConfigurations : IEntityTypeConfiguration<ProductItemInventory>
    {
        public void Configure(EntityTypeBuilder<ProductItemInventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(i => i.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProductItemId(x));
        }
    }
}
