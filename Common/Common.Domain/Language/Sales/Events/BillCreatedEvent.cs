using Common.Domain.Base;
using Common.Domain.Language.Sales.ValueObjects;

namespace Common.Domain.Language.Sales.Events
{
    public record BillCreatedEvent : EventBase
    {
        public BillId Id { get; private init; }
        public BillCreatedEvent(BillId id)
        {
            Id = id;
        }

    }
}
