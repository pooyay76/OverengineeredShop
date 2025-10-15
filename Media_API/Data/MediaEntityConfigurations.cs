using Media_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Media_API.Data
{
    public class MediaEntityConfigurations : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Medias");
            builder.Property(x => x.Path).HasMaxLength(127).IsRequired();
            builder.Property(x => x.Section).HasMaxLength(63).IsRequired();

        }
    }
}
