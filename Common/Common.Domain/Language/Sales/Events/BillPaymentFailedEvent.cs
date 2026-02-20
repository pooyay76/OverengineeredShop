using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Common.Domain.Language.Sales.Events
{
    public record BillPaymentFailedEvent : EventBase
    {
        public BillId Id { get; private init; }


        public BillPaymentFailedEvent(BillId id) : base()
        {
            Id = id;
        }

    }
}
