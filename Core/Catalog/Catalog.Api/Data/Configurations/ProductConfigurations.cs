using Catalog.Api.Models;
using Catalog.Api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{
    public partial class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(150);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.PictureMediaAddress).HasMaxLength(500);
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId).IsRequired(false);
            builder.HasMany(x => x.Items).WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }

    }
}
