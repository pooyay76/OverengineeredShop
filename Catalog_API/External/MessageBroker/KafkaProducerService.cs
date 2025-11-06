using Catalog.Api.Contracts.Interfaces;
using Confluent.Kafka;

namespace Catalog.Api.External.MessageBroker
{
    public class KafkaProducerService: IKafkaProducerService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        private readonly ILogger<KafkaProducerService> _logger;
        public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
            _logger = logger;
        }

        public async Task ProduceAsync(string key, string value)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            using var producer = new ProducerBuilder<string, string>(config).Build();
            var message = new Message<string, string> { Key = key, Value = value };

            var deliveryResult = await producer.ProduceAsync(_topic, message);
            _logger.LogInformation($"Delivered '{deliveryResult.Key}:{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}, status:{deliveryResult.Status}");
        }
    }
}
