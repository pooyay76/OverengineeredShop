using Media.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Media.Api.Data
{
    public class MediaEntityConfigurations : IEntityTypeConfiguration<MediaEntity>
    {
        public void Configure(EntityTypeBuilder<MediaEntity> builder)
        {
            builder.ToTable("Medias");
            builder.Property(x => x.Path).HasMaxLength(127).IsRequired();
            builder.Property(x => x.Section).HasMaxLength(63).IsRequired();

        }
    }
}
