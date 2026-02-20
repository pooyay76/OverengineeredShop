using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.Configurations
{
    public class EventEntityTypeConfigurations : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(e => e.EventId);
            builder.Property(e => e.EventType)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.EventData)
                .HasColumnType("jsonb").HasMaxLength(16384).IsRequired();
            builder.Property(e => e.OccurredOn)
                .IsRequired();
            builder.Property(e => e.Version)
                .IsRequired();
            builder.Property(e => e.AggRootId)
                .IsRequired();
            builder.Property(e => e.AggRootType).HasMaxLength(200).IsRequired();
        }
    }
}
