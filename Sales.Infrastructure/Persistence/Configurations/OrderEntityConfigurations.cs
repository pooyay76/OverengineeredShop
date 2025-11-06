using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.OrderAgg.Models;
namespace Sales.Infrastructure.Persistence.Configurations
{
    public class OrderEntityConfigurations : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new OrderId(x));
            builder.Property(x => x.BillId).HasConversion(x => x.Value, x => new BillId(x));
            builder.Property(x => x.CustomerId).HasConversion(x => x.Value, x => new CustomerId(x));

            builder.Property(x => x.OrderStatus).HasConversion<string>();

            builder.OwnsMany(x => x.OrderItems, x =>
            {
                x.WithOwner(y=>y.Order);
                x.ToTable("OrderItems");
                x.Property(y => y.Id).HasConversion(y => y.Value, y => new OrderItemId(y));
                x.Property(y => y.ProductItemId).HasConversion(y => y.Value, y => new ProductItemId(y));
            });

        }
    }
}
