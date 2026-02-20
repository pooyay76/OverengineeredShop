using Catalog.Api.Models;
using Common.Domain.Language.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{
    public class ProductTypeConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PictureMediaAddress).HasMaxLength(500);
            builder.Property(x=>x.Id).HasConversion(x=>x.Value,x=>new ProductId(x)).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId).IsRequired(false);
            builder.HasMany(x => x.ProductItems).WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
           
        }

    }
}
