using Common.Application.Contracts;
using Common.Application.Helpers;
using Common.Domain.Base;
using Common.Domain.Contracts;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Common.Application
{
    public class EventPublisher
    {
        private readonly IEventRepository eventRepository;
        private readonly IOutboxRepository outboxEntityRepository;
        private readonly IFrameworkUnitOfWork unitOfWork;
        private readonly ILogger<EventPublisher> _logger;
        private readonly Mediator mediator;
        public EventPublisher(IEventRepository eventRepository, IOutboxRepository outboxEntityRepository,
            IFrameworkUnitOfWork shopSystemUnitOfWork, Mediator mediator, ILogger<EventPublisher> logger)
        {
            this.eventRepository = eventRepository;
            this.outboxEntityRepository = outboxEntityRepository;
            this.unitOfWork = shopSystemUnitOfWork;
            this.mediator = mediator;
            _logger = logger;
        }
        public async Task<bool> PublishEventAsync<T>(T @event) where T : EventBase
        {
            try
            {

            var eventEntity = new EventEntity
            {
                EventId = @event.EventId,
                EventType = typeof(T).Name,
                EventData = EventSerializer.ToJson(@event),
                Version = @event.Version,
                SchemaVersion = @event.SchemaVersion,
                IsGlobal = @event is IntegrationEventBase,
                AggRootId = @event.AggRootId,
                AggRootType = @event.AggRootType
            };
            await eventRepository.StoreAsync(eventEntity);

            if (@event is IntegrationEventBase)
            {
                OutboxEntity outboxEntity = new OutboxEntity
                {
                    DestinationTopic = GetKafkaTopicName(@event.GetType()),
                    MessageId = eventEntity.EventId.ToString(),
                    MessageValue = eventEntity.EventData
                };
                await outboxEntityRepository.AddAsync(outboxEntity);
            }

            await unitOfWork.CommitAsync();

            }
            catch
            {
                _logger.LogError("Could not push events to kafka");
                return false;
            }

            await mediator.PublishAsync(@event);
            return true;

        }
        public async Task<bool> PublishEventsAsync<T>(List<T> events) where T : EventBase
        {
            var sortedEvents = events.OrderBy(x=>x.OccurredOn);


            try
            {
                foreach (var @event in sortedEvents)
                {
                    var eventEntity = new EventEntity
                    {
                        EventType = @event.GetType().Name,
                        EventData = EventSerializer.ToJson(@event),
                        Version = @event.Version,
                        SchemaVersion = @event.SchemaVersion,
                        IsGlobal = @event is IntegrationEventBase,
                        AggRootId = @event.AggRootId,
                        AggRootType = @event.AggRootType,
                        EventId = @event.EventId
                    };

                    //the following doesnt commit because we might want to persist outbox entities too if the event is global
                    await eventRepository.StoreAsync(eventEntity);
                    if (eventEntity.IsGlobal)
                    {
                        await outboxEntityRepository.AddAsync(new OutboxEntity
                        {
                            DestinationTopic = GetKafkaTopicName(@event.GetType()),
                            MessageId = eventEntity.EventId.ToString(),
                            MessageValue = eventEntity.EventData
                        });
                    }
                    await unitOfWork.CommitAsync();
                }


            }
            catch (Exception ex)
            {
                // Log or handle the error for this specific event
                // You can decide to continue processing other events or stop entirely
                _logger.LogError(ex, "Error processing event");
                return false;
            }
            //iterate again after commit for publishing
            foreach (var @event in sortedEvents)
            {
                await mediator.PublishAsync(@event);
            }
            return true;

        }


        private static string GetKafkaTopicName(Type type)
        {
            string typeName = type.Name;

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException();
            }

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < typeName.Length; i++)
            {
                char currentChar = typeName[i];

            
                if (Char.IsUpper(currentChar))
                {
                    if (i > 0)
                    {
                        result.Append('-'); 
                    }

                    result.Append(Char.ToLower(currentChar)); 
                }
                else
                {
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }
    }
}
