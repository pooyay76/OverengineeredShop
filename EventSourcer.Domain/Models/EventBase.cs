

namespace Warehouse.Domain.Core
{
    public abstract record Event
    {
        public Guid Id { get; protected init; }
        public string EventType { get; protected init; }
        public Guid AggregateId { get; protected init; }
        public string AggregateType { get; protected init; }

    }
}
