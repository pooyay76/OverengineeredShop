namespace Common.Domain.Base
{
    //used in interceptors to identify aggregate roots
    public interface IEventDrivenAggregateRoot
    {
        public IReadOnlyCollection<EventBase> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
