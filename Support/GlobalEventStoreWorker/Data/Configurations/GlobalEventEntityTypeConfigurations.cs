using GlobalEventStoreWorker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalEventStoreWorker.Data.Configurations
{
    public class GlobalEventEntityTypeConfigurations : IEntityTypeConfiguration<GlobalEventEntity>
    {

        public void Configure(EntityTypeBuilder<GlobalEventEntity> builder)
        {
            builder.ToTable("GlobalEvents");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TopicName).HasMaxLength(128).IsRequired();
            builder.Property(e => e.EventData).HasMaxLength(16384).IsRequired();    
        }
    }
}
