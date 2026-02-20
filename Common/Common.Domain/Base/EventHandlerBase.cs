namespace Common.Domain.Base
{
    public abstract class EventHandlerBase<TEvent>
    where TEvent : EventBase

    {
        public abstract Task HandleAsync(TEvent request);

    }
}
