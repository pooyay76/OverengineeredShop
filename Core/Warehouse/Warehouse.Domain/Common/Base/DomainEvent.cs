namespace Warehouse.Domain.Common.Base
{
    public record DomainEvent<AggregateKeyT> : IDomainEvent
        where AggregateKeyT : StronglyTypedId
    {
        public AggregateKeyT AggregateId { get; init; }

        public DomainEvent(AggregateKeyT id)
        {
            AggregateId = id;
        }
    }
}
