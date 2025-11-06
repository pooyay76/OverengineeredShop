
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.PriceLabelAgg.Models;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.External.MessageBroker
{
    public class KafkaConsumerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaConsumerService(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ConsumeAsync()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest, 
                EnableAutoCommit = true,
                GroupId = "1"
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topic);

            try
            {
                while (true)
                {
                    var result = consumer.Consume();
                    Console.WriteLine($"Consumed message: {result.Message.Value} at offset {result.Offset}");
                    PriceLabel price = new(new ProductItemId(long.Parse(result.Message.Key.ToString())), new Money(decimal.Parse(result.Message.Value)));
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        SalesDbContext salesDbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
                        await salesDbContext.ProductItemPrices.AddAsync(price);
                        await salesDbContext.SaveChangesAsync();
                    }
                }

            }
            catch
            {
                consumer.Close();
            }
        }
    }
}
