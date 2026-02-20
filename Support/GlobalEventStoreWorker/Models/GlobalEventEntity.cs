namespace GlobalEventStoreWorker.Models
{
    public record GlobalEventEntity
    {
        public long Id { get; private set; }
        public string TopicName { get; init; }
        public string EventData { get; init; }

        public GlobalEventEntity(string topicName, string eventData)
        {
            TopicName = topicName;
            EventData = eventData;
        }

        private GlobalEventEntity()
        {
        }
    }
}
