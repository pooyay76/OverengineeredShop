using Common.Domain.Base;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.MessageBroker
{
    public class KafkaEventConsumerBackgroundService<TEvent> : BackgroundService where TEvent : EventBase
    {
        private readonly KafkaEventConsumerService<TEvent> kafkaConsumerService;
        private readonly ILogger<KafkaEventConsumerBackgroundService<TEvent>> logger;
        public KafkaEventConsumerBackgroundService(KafkaEventConsumerService<TEvent> kafkaConsumerService,
            ILogger<KafkaEventConsumerBackgroundService<TEvent>> logger)
        {
            this.kafkaConsumerService = kafkaConsumerService;
            this.logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"{GetType().Name} is running");
            await kafkaConsumerService.ConsumeAsync();
        }
    }
}
