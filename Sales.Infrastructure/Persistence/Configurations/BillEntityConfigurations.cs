using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.DiscountAgg.Models;
namespace Sales.Infrastructure.Persistence.Configurations
{
    public class BillEntityConfigurations : IEntityTypeConfiguration<Bill>
    {

        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new BillId(x));
            builder.Property(x => x.BillDiscountId).HasConversion(x => x.Value, x => new DiscountId(x));

            builder.Property(x => x.CustomerId).HasConversion(x => x.Value, x => new CustomerId(x));
            //foreach (var property in typeof(Bill).GetProperties())
            //{
            //    // Check if the property type is Money
            //    if (property.PropertyType == typeof(Money))
            //    {
            //        // Apply conversion to Money properties
            //        builder
            //            .Property<Money>(property.Name)
            //            .HasConversion(
            //                v => v.GetValue(),  // Convert Money to decimal for storage
            //                v => new Money(v)   // Convert decimal to Money on retrieval
            //            );
            //    }
            //}
            builder.OwnsOne(x => x.ShippingInformation).WithOwner();

        }
    }
}
