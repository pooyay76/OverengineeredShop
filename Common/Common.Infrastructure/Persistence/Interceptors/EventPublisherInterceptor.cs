using Common.Application;
using Common.Domain.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Common.Infrastructure.Persistence.Interceptors
{
    public class EventPublisherInterceptor : SaveChangesInterceptor
    {
        private readonly EventPublisher eventPublisher;
        public EventPublisherInterceptor(EventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            throw new NotImplementedException("Use the async version of this method");
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
              DbContextEventData eventData,
              InterceptionResult<int> result,
              CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return result;


            IEnumerable<IEventDrivenAggregateRoot> aggregatesWithEvents = context.ChangeTracker
                .Entries<IEventDrivenAggregateRoot>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Count > 0)
                .Select(e => e.Entity);

            //no need to sort as these will be sorted inside the publisher class
            var events = aggregatesWithEvents.SelectMany(x => x.DomainEvents).ToList();

            var pubResult = await eventPublisher.PublishEventsAsync(events);

            if(pubResult == false)
            {
                throw new Exception("Could not publish events");
            }
         
            foreach (var aggregate in aggregatesWithEvents)
            {
                aggregate.ClearDomainEvents();
            }

            return await base.SavingChangesAsync(eventData,result,cancellationToken);
        }
    }
}
