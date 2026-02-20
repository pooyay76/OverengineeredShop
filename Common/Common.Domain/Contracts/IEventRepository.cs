using Common.Domain.Models;

namespace Common.Domain.Contracts
{
    public interface IEventRepository
    {
        Task StoreAsync(EventEntity @event);
        Task StoreRangeAsync(IEnumerable<EventEntity> events);
    }
}
