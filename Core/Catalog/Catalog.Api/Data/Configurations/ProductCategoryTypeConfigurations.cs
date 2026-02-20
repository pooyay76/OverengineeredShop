using Catalog.Api.Models;
using Common.Domain.Language.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{
    public class ProductCategoryTypeConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.HasMany(x => x.Products).WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId).IsRequired(false);
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProductCategoryId()).ValueGeneratedOnAdd();

        }
    }
}
