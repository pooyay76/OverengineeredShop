using Common.Domain.Contracts;
using Warehouse.Domain.Core;

namespace Common.Infrastructure
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public List<EventBase> GetByAggregateId(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public EventBase GetById(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public void Save(List<EventBase> events)
        {
            throw new NotImplementedException();
        }
    }
}
