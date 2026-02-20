namespace Common.Domain.Base
{
    public abstract record EventBase
    {
        public Guid EventId { get;private init; } 
        public DateTimeOffset OccurredOn { get;private init; } = DateTimeOffset.UtcNow;
        public string AggRootType { get; set; }
        public Guid AggRootId { get; set; }
        public int Version { get; set; }
        public int SchemaVersion { get; internal set; }

        protected EventBase()
        {
            EventId = Guid.NewGuid();
        }
    }

}
