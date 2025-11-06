
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.DiscountAgg.Models;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class BillItemEntityConfigurations : IEntityTypeConfiguration<BillItem>
    {
        public void Configure(EntityTypeBuilder<BillItem> builder)
        {
            builder.ToTable("BillItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new BillItemId(x));
            builder.Property(x => x.ProductItemId)
                .HasConversion(x => x.Value, x => new ProductItemId(x));
            builder.Property(x => x.DiscountId).HasConversion(x => x.Value, x => new DiscountId(x));

            builder.Property(x=>x.BillId).HasConversion(x=>x.Value,x=>new BillId(x));
        }
    }
}
