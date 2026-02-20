using Common.Application.Contracts;

using Common.Domain.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Common.Infrastructure.MessageBroker
{

    public class KafkaProducer : IEventProducerService
    {
        private readonly string _bootstrapServers;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(IConfiguration configuration,
            ILogger<KafkaProducer> logger)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"] ?? 
                throw new ArgumentNullException("Kafka bootstrap server config not found");
            _logger = logger;
        }

        public async Task ProduceAsync(string topic, string key,string value)
        {
            var config = new ProducerConfig { 
                BootstrapServers = _bootstrapServers,
            Acks= Acks.All
            };

            using var producer = new ProducerBuilder<string, string>(config).Build();
            var message = new Message<string, string> { Key = key, Value = value};

            var deliveryResult = await producer.ProduceAsync(topic, message);
            if (deliveryResult.Status == PersistenceStatus.Persisted)
                _logger.LogInformation($"Delivered '{deliveryResult.Message.Key}:{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}");
            else
                _logger.LogCritical($"Message delivery failed with status: {deliveryResult.Status}");
        }
        public async Task ProduceRangeAsync(List<OutboxEntity> messages)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                Acks = Acks.All,
                TransactionalId = Guid.NewGuid().ToString()
            };
            try
            {

                using var producer = new ProducerBuilder<string, string>(config).Build();
                producer.BeginTransaction();

                foreach (var message in messages)
                {

                    var m = new Message<string, string>
                    {
                        Key = message.MessageId,
                        Value = message.MessageValue
                    };
                    var deliveryResult = await producer.ProduceAsync(message.DestinationTopic, m);
                    _logger.LogInformation($"Delivered '{deliveryResult.Message.Key}:{deliveryResult.Value}' " +
                        $"to '{deliveryResult.TopicPartitionOffset}");
                }
                producer.CommitTransaction();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error producing messages in transaction");
                throw;
            }

        }
    }
}
