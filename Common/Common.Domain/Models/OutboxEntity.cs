using Common.Domain.Base;

namespace Common.Domain.Models
{
    public class OutboxEntity 
    {
        public long Id { get; set; }
        public string DestinationTopic { get;init; }
        public string MessageId { get;init; }
        public string MessageValue { get;init; }
        public DateTime CreatedAt { get;} = DateTime.UtcNow;

    }
}
