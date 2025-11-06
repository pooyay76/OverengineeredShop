namespace Catalog.Api.Contracts.Interfaces
{
    public interface IWarehouseClient
    {
        Task<List<Guid>> GetAvailable(List<Guid> ids);

    }
}
