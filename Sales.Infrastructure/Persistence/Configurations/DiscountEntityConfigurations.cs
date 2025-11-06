
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Common;
using Sales.Domain.DiscountAgg.Models;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class DiscountEntityConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new DiscountId(x));
            builder.Property(x => x.DiscountType).HasConversion<string>();
            builder.Property(x => x.ProductItemId)
                .HasConversion(x => x.Value, x => new ProductItemId(x));


        }
    }
}
