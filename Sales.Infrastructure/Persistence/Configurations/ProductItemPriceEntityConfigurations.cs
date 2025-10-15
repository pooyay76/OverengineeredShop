using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.PriceHistoryAgg.Models;
namespace Sales.Infrastructure.Persistence.Configurations
{
    public class ProductItemPriceEntityConfigurations : IEntityTypeConfiguration<PriceLabel>
    {

        public void Configure(EntityTypeBuilder<PriceLabel> builder)
        {
            builder.ToTable("ProductItemPrices");
            builder.HasKey(x => x.Id);
        }
    }
}
