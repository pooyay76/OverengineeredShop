namespace Catalog.Api.Contracts.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string key, string value);
    }
}
