using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Feedback.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Feedback.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Api.Data.Configurations
{
    public class CommentTypeConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion<Guid>(
                    id => id.Value,
                    value => new CommentId(value))
                .ValueGeneratedOnAdd();
            builder.Property(c => c.AuthorId)
                .HasConversion<Guid>(
                    id => id.Value,
                    value => new UserId(value))
               .IsRequired();
            builder.Property(x=>x.ProductId)
                                .HasConversion<Guid>(
                    id => id.Value,
                    value => new ProductId(value))
               .IsRequired();
            builder.Property(x => x.BodyText).HasMaxLength(300).IsRequired();
            builder.Property(x => x.AuthorName).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(120).IsRequired();

        }
    }
}
