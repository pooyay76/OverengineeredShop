using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class PaymentTransactionEntityConfigurations : IEntityTypeConfiguration<PaymentSession>
    {

        public void Configure(EntityTypeBuilder<PaymentSession> builder)
        {
            builder.ToTable("PaymentTransactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SessionStatus).HasConversion<string>();
            builder.HasOne(x => x.Order).WithMany(x => x.PaymentTransactions)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
