using Common.Domain.Contracts;
using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly FrameworkDbContext _context;

        public EventRepository(FrameworkDbContext context)
        {
            _context = context;
        }

        public async Task StoreAsync(EventEntity @event)
        {

            int last_version  = _context.Events
                .Where(x => x.AggRootType == @event.AggRootType && x.AggRootId == @event.AggRootId)?
                .Select(x=>x.Version)?.OrderByDescending(x=>x)?.FirstOrDefault() ?? 0;

            //version starts at 1
            int next_version = last_version + 1;

            if (@event.Version != next_version)
            {
                throw new InvalidOperationException(
                    $"Version conflict detected for aggregate root {@event.AggRootType} with ID {@event.AggRootId}." +
                    $" Expected version: {next_version + 1}, but got {@event.Version}.");
            }

            await _context.Events.AddAsync(@event);
        }
        public async Task StoreRangeAsync(IEnumerable<EventEntity> events)
        {

            List<EventEntity> firstEventsOfEachAgg = events.GroupBy(x => new { x.AggRootType ,x.AggRootId}).Select(x => x.MinBy(y => y.Version)).ToList();

            var aggRootObjectEvents = _context.Events

                .Where(x => firstEventsOfEachAgg.Select(e => e.AggRootId).Contains(x.AggRootId) && 
                firstEventsOfEachAgg.Select(e => e.AggRootType).Contains(x.AggRootType))
                .GroupBy(x => new { x.AggRootType, x.AggRootId })
                .Select(g => new
                {
                    AggRootType = g.Key.AggRootType,
                    AggRootId = g.Key.AggRootId,
                    MaxVersion = g.Max(x => x.Version)
                }).ToList();

            
            var anyUnexpectedVersion = firstEventsOfEachAgg.Any(e =>
            {
                var aggRootEvent = aggRootObjectEvents.FirstOrDefault(x => x.AggRootId == e.AggRootId && x.AggRootType == e.AggRootType);
                int expectedVersion = aggRootEvent != null ? aggRootEvent.MaxVersion + 1 : 1;
                return e.Version != expectedVersion;
            });

            if(anyUnexpectedVersion)
                throw new InvalidOperationException(
                    $"Version conflict detected for one or more aggregate roots. " +
                    $"Please ensure that the versions of the events being stored are " +
                    $"sequential and consistent with the existing events in the database.");

            await _context.Events.AddRangeAsync(events);

        }

    }
}
