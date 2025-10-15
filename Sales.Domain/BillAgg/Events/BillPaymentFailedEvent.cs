
using Sales.Domain._common.Base;
using Sales.Domain.BillAgg.Models;

namespace Sales.Domain.BillAgg.Events
{
    public record BillPaymentFailedEvent : DomainEvent<BillId>
    {
        public BillPaymentFailedEvent(BillId id) : base(id)
        {
        }
    }
}
