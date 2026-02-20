using Common.Domain.Base;

namespace Common.Domain.Models
{
    public class EventEntity
    {
        public Guid EventId { get; init; }
        public Guid AggRootId { get;init; }
        public string AggRootType { get;init; }
        public string EventType { get;init; }
        public string EventData { get;init; }
        public bool IsGlobal { get; init; }
        public DateTimeOffset OccurredOn { get; private init; } = DateTimeOffset.UtcNow;
        public int Version { get; init; }
        public int SchemaVersion { get; init; }
    }
}
