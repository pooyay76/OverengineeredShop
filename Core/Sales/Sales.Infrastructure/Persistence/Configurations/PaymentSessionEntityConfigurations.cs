using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.PaymentSessionAgg.Models;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class PaymentSessionEntityConfigurations : IEntityTypeConfiguration<PaymentSession>
    {

        public void Configure(EntityTypeBuilder<PaymentSession> builder)
        {
            builder.ToTable("PaymentTransactions");
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => new PaymentSessionId(x));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.SessionStatus).HasConversion<string>();
        }
    }
}
