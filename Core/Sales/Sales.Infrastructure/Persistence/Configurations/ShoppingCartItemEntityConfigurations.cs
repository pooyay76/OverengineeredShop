using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Common;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class ShoppingCartItemEntityConfigurations : IEntityTypeConfiguration<ShoppingCartItem>
    {

        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.ToTable("ShoppingCartItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new CustomerId(x));
            builder.Property(x => x.ProductItemId).HasConversion(x => x.Value, x => new ProductItemId(x));

        }
    }
}
