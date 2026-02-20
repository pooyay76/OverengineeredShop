using Common.Domain.Contracts;
using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly FrameworkDbContext _context;

        public OutboxRepository(FrameworkDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxEntity @event)
        {
            await _context.OutboxEntities.AddAsync(@event);
        }

        public async Task AddRangeAsync(IEnumerable<OutboxEntity> events)
        {
            await _context.OutboxEntities.AddRangeAsync(events);
        }

        public async Task<List<OutboxEntity>> GetAllAsync()
        {
            return await _context.OutboxEntities.ToListAsync();
        }

        public void RemoveRange(List<OutboxEntity> messages)
        {
            _context.OutboxEntities.RemoveRange(messages);
        }


    }
}
