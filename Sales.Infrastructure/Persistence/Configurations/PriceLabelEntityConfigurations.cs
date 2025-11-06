using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Common;
using Sales.Domain.PriceLabelAgg.Models;
namespace Sales.Infrastructure.Persistence.Configurations
{
    public class PriceLabelEntityConfigurations : IEntityTypeConfiguration<PriceLabel>
    {

        public void Configure(EntityTypeBuilder<PriceLabel> builder)
        {
            builder.ToTable("ProductItemPrices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductItemId).HasConversion(x => x.Value, x => new ProductItemId(x));

        }
    }
}
