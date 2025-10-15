using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.ShoppingCartAgg.Models;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class ShoppingCartEntityConfigurations : IEntityTypeConfiguration<ShoppingCart>
    {

        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("ShoppingCarts");
            builder.HasKey(x => x.Id);
            builder.OwnsMany(x => x.ShoppingCartItems, y =>
            {
                y.ToTable("ShoppingCartItems");
                y.HasKey(z => z.Id);
            });
        }
    }
}
