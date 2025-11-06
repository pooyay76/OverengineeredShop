using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Events
{
    public record BillPaymentFailedEvent : DomainEvent<BillId>
    {
        public BillPaymentFailedEvent(BillId id) : base(id)
        {

        }
    }
}
