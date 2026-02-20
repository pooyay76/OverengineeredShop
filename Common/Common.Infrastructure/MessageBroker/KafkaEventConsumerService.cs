
using Common.Application;
using Common.Application.Helpers;
using Common.Domain.Base;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.MessageBroker
{
    public class KafkaEventConsumerService<TEvent> where TEvent : EventBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _bootstrapServers;
        private readonly string _groupId;
        private readonly string _topic;

        private readonly ILogger<KafkaEventConsumerService<TEvent>> logger;
        public KafkaEventConsumerService(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory, 
            ILogger<KafkaEventConsumerService<TEvent>> logger)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _serviceScopeFactory = serviceScopeFactory;
            _groupId = configuration["Kafka:GroupId"];
            this.logger = logger;
            var section = configuration.GetSection("Kafka:Topics");
            _topic = section[typeof(TEvent).Name] ??
                throw new ArgumentNullException($"Kafka topic for {typeof(TEvent).Name} event not found");

        }

        public async Task ConsumeAsync()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                GroupId = _groupId
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topic);
            while (true)
            {
                try
                {
                    var result = consumer.Consume();
                    Console.WriteLine($"Consumed message: {result.Message.Value} at offset {result.Offset}");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        Mediator mediator = scope.ServiceProvider.GetRequiredService<Mediator>();
                        await mediator.PublishAsync(result.Message.Value.ToObject<TEvent>());
                    }

                    consumer.Commit(result);
                    logger.LogInformation($"Event of type {typeof(TEvent).Name} processed and published to mediator successfully.");

                }
                catch (Exception ex)
                {
                    logger.LogError($"Background service for event {typeof(TEvent).Name} failed! ex: {ex.InnerException}");
                    consumer.Close();
                }
            }
        }
    }
}
