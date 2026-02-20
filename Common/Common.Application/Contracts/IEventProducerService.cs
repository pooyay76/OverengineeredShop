using Common.Domain.Base;
using Common.Domain.Models;

namespace Common.Application.Contracts
{

    public interface IEventProducerService
    {
        Task ProduceAsync(string topic, string key, string value);
        Task ProduceRangeAsync(List<OutboxEntity> messages);
    }
}
