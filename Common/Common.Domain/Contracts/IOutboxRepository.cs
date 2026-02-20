
using Common.Domain.Models;

namespace Common.Domain.Contracts
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxEntity @event);
        Task AddRangeAsync(IEnumerable<OutboxEntity> events);
        Task<List<OutboxEntity>> GetAllAsync();
        void RemoveRange(List<OutboxEntity> messages);
    }
}
