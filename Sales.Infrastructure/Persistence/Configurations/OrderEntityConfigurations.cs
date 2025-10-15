using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.OrderAgg.Models;
namespace Sales.Infrastructure.Persistence.Configurations
{
    public class OrderEntityConfigurations : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderStatus).HasConversion<string>();
            builder.HasOne(x => x.ShoppingCart).WithOne().HasForeignKey<Order>(x => x.ShoppingCartId);
            builder.OwnsOne(x => x.ReceiverInfo, y =>
            {
                y.ToTable("ReceiverInformation");
                y.HasKey(z => z.Id);

                y.Property(z => z.Province).HasMaxLength(60).IsRequired();
                y.Property(z => z.City).HasMaxLength(60).IsRequired();
                y.Property(z => z.Address).HasMaxLength(250).IsRequired();
                y.Property(z => z.PostalCode).HasMaxLength(25).IsRequired();
                y.Property(z => z.ContactPhoneNumber).HasMaxLength(60).IsRequired();
                y.Property(z => z.ReceiverFirstName).HasMaxLength(60).IsRequired();
                y.Property(z => z.ReceiverLastName).HasMaxLength(60).IsRequired();

            });
        }
    }
}
