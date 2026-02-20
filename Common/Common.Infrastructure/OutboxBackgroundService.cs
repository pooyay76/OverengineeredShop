using Common.Application.Contracts;
using Common.Domain.Contracts;
using Common.Domain.Models;
using Common.Infrastructure.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure
{
    public class OutboxBackgroundService : BackgroundService
    {
        private readonly ILogger<OutboxBackgroundService> _logger;
        private readonly TimeSpan _delay = ConfigValues.EventConsumerBackgroundDelay;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OutboxBackgroundService(ILogger<OutboxBackgroundService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;

            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var scope = _serviceScopeFactory.CreateScope())
            {

                IEventProducerService _eventProducerService = scope.ServiceProvider.GetRequiredService<IEventProducerService>();
                IOutboxRepository  _outboxEntityRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                IFrameworkUnitOfWork  _unitOfWork = scope.ServiceProvider.GetRequiredService<IFrameworkUnitOfWork>();

                List<OutboxEntity> messages = await _outboxEntityRepository.GetAllAsync();
                if (messages.Any())
                {

                    _outboxEntityRepository.RemoveRange(messages);
                    await _eventProducerService.ProduceRangeAsync(messages);
                    await _unitOfWork.CommitAsync();
                    foreach (var message in messages)
                    {
                        _logger.LogInformation($"Outbox message with Id: {message.MessageId} has been sent to topic: " +
                            $"{message.DestinationTopic}");
                    }
                }
            }

            await Task.Delay(_delay, stoppingToken);
        }
    }
}
