using Common.Domain.Base;
using Confluent.Kafka;
using GlobalEventStoreWorker.Data;
using GlobalEventStoreWorker.Models;

namespace GlobalEventStoreWorker
{
    public class KafkaEventConsumerWorker:BackgroundService
    {
        private readonly string _bootstrapServers;
        private readonly string _groupId;
        private readonly ILogger<KafkaEventConsumerWorker> logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public KafkaEventConsumerWorker(IConfiguration configuration,
            ILogger<KafkaEventConsumerWorker> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _groupId = configuration["Kafka:GroupId"];
            this.logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<string> _topics = typeof(IntegrationEventBase).Assembly.GetTypes().Where(x =>
             x.IsSubclassOf(typeof(IntegrationEventBase)) && !x.IsAbstract).Select(x => x.Name).ToList();
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                GroupId = _groupId
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topics);
            while (true)
            {
                try
                {
                    var result = consumer.Consume();

                    logger.LogInformation($"Consumed message: {result.Message.Value} at offset {result.Offset}");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<GlobalEventContext>();
                        var @event = new GlobalEventEntity(result.Topic,result.Message.Value);
                        await dbContext.GlobalEvents.AddAsync(@event);
                        await dbContext.SaveChangesAsync();
                    }
                    consumer.Commit(result);

                }
                catch (Exception ex)
                {
                    consumer.Close();
                    logger.LogCritical($"Background service for global event db failed! ex: {ex.InnerException}");
                }
            }
        }
    }
}
