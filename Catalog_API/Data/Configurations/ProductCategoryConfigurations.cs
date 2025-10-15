using Catalog_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_API.Data.Configurations
{
    public class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(150);
            builder.HasMany(x => x.Products).WithOne(x => x.ProductCategory)
            .HasForeignKey(x => x.ProductCategoryId).IsRequired(false);
        }
    }
}
