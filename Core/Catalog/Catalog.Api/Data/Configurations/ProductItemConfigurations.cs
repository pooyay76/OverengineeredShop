using Catalog.Api.Models;
using Catalog.Api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Catalog.Api.Data.Configurations
{
    public partial class ProductConfigurations
    {
        public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
        {
            public void Configure(EntityTypeBuilder<ProductItem> builder)
            {
                builder.HasKey(x => x.Id);
                builder.OwnsOne( x=>x.Price, navigationBuilder =>
                {
                    navigationBuilder.Property(x=>x.Currency).HasMaxLength(3);
                });

            }
        }
        public static Money MoneyConverter(string money)
        {
            var parts = money.Split(':');
            return new Money(decimal.Parse(parts[0]), parts[1]);
        }
    }
}
