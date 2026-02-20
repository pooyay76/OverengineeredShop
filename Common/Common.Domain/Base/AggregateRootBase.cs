using Common.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.Base
{
    public abstract class AggregateRootBase<TKey> : EntityBase<TKey>, IEventDrivenAggregateRoot
        where TKey : StronglyTypedId,new()
    {
        //not persisted, stores in RAM
        private List<string> _errors = [];
        //not persisted, stores in RAM
        private List<EventBase> events = [];
        private bool isValid => _errors.Count() == 0;

        //used for detecting concurrency problems
        public int Version { get; private set; } = 1;

        //manually set this in your agg root constructor and increment it every time you
        //create a new migration during production
        public int SchemaVersion { get; protected set; } = 1;
        [NotMapped]
        public IReadOnlyCollection<EventBase> DomainEvents => events.AsReadOnly();
        protected AggregateRootBase()
        {
        }

        public void AddError(string message)
        {
            _errors.Add(message);
        }



        private void Apply(EventBase @event)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
            }

            method.Invoke(this, new object[] { @event });

        }
        protected void ReplayEvents(List<EventBase> events)
        {
            foreach(var item in  events)
            {
                Apply(item);
            }
        }

        protected void AddEvent(EventBase domainEvent)
        {
            //Events are stored in a list inside aggregate root
            //Before saving changes interceptor calls event store which
            //publishes events to kafka and clears list using outbox pattern



            //validations are done before this step, but i wait until here to throw an exception.
            //now we have to read all the errors that are added to the list and
            //throw an exception if there are any errors,
            //so that the transaction can be rolled back and no events are added to the list
            if (isValid == false) 
            { 
                throw new DomainException(_errors);
            }


            //object will have version of 1 when created, and every time an event is added, version will be incremented by 1
            domainEvent.Version = Version;
            domainEvent.AggRootId = Id.Value;
            domainEvent.SchemaVersion = SchemaVersion;

            //careful not to change the name of the aggregate root class in production,
            //otherwise it will break event sourcing
            domainEvent.AggRootType = GetType().Name;

            //events are applied after they are added to the list, so that they can be replayed when needed
            Apply(domainEvent);

            events = events ?? new List<EventBase>();
            events.Add(domainEvent);
            Version++;

        }


        /// <summary>
        /// To be used only by event store
        /// </summary>
        public void ClearDomainEvents()
        {
            events.Clear();
        }
    }
}
