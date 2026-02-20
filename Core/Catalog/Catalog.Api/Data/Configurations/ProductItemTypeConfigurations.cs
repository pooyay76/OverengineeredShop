using Catalog.Api.Models;
using Common.Domain.Language.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{

        public class ProductItemTypeConfigurations : IEntityTypeConfiguration<ProductItem>
        {
            public void Configure(EntityTypeBuilder<ProductItem> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProductItemId(x)).ValueGeneratedOnAdd();
                builder.OwnsOne( x=>x.Price, navigationBuilder =>
                {
                    navigationBuilder.Property(x=>x.Currency).HasMaxLength(3);
                });

            }
        

    }
}
