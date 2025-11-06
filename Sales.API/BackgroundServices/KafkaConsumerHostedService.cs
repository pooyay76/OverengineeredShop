
using Sales.Infrastructure.External.MessageBroker;

namespace Sales.Api.BackgroundServices
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly KafkaConsumerService kafkaConsumerService;
        private readonly ILogger<KafkaConsumerHostedService> logger;
        public KafkaConsumerHostedService(KafkaConsumerService kafkaConsumerService, ILogger<KafkaConsumerHostedService> logger)
        {
            this.kafkaConsumerService = kafkaConsumerService;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Running");
             await kafkaConsumerService.ConsumeAsync();
        }
    }
}
