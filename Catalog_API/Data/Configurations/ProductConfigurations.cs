using Catalog_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_API.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.HasOne(x => x.ProductCategory).WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId).IsRequired(false);
        }
    }
}
