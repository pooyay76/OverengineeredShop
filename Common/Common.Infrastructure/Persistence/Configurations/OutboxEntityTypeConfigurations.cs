using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Persistence.Configurations
{
    public class OutboxEntityTypeConfigurations : IEntityTypeConfiguration<OutboxEntity>
    {
        public void Configure(EntityTypeBuilder<OutboxEntity> builder)
        {
            builder.ToTable("Outbox");
            builder.Property(x => x.MessageValue).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.MessageId).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.DestinationTopic).HasMaxLength(100).IsRequired();
        }
    }
}
