using Common.Domain.Global.ValueObjects;
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
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));


        }
    }
}
