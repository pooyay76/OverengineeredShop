using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sales.Domain._common.Base;

namespace Sales.Infrastructure.Persistence.Interceptors
{
    public class DomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator mediator;

        public DomainEventInterceptor(IMediator mediator)
        {
            this.mediator = mediator;
        }


        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
              DbContextEventData eventData,
              InterceptionResult<int> result,
              CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return result;

            // Get aggregate roots with events
            var aggregatesWithEvents = context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Count > 0)
                .Select(e => e.Entity)
                .ToList();

            // Publish and clear events
            foreach (var aggregate in aggregatesWithEvents)
            {
                var events = aggregate.DomainEvents.ToList(); // Clone to avoid mutation during iteration

                foreach (var domainEvent in events)
                {
                    await mediator.Publish(domainEvent, cancellationToken);
                }
                aggregate.ClearDomainEvents();
            }

            return result;
        }
    }
}
