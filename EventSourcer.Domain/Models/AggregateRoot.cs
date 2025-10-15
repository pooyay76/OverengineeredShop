namespace Warehouse.Domain.Core
{
    public abstract class AggregateRoot : Entity
    {
        private List<EventBase> _changes = [];

        public int Version { get; set; } = -1;

        public List<EventBase> GetUncommittedChanges()
        {
            return _changes;
        }
        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        public void RaiseEvent(EventBase @event)
        {
            ApplyEvent(@event, false);
        }
        public void ReplayEvent(EventBase @event)
        {
            ApplyEvent(@event, true);
        }
        public void ApplyEvent(EventBase @event, bool is_replay)
        {
            var method = GetType().GetMethod("Apply", [@event.GetType()]);
            if (method == null)
            {
                throw new Exception($"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
            }


            method.Invoke(this, [@event]);


            if (is_replay == false)
                _changes.Add(@event);
        }
    }
}
