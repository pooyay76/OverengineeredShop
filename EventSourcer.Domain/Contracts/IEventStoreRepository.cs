using Warehouse.Domain.Core;

namespace Common.Domain.Contracts
{
    public interface IEventStoreRepository
    {
        void Save(List<EventBase> events);
        List<EventBase> GetByAggregateId(Guid aggregateId);
        EventBase GetById(Guid eventId);

    }
}
