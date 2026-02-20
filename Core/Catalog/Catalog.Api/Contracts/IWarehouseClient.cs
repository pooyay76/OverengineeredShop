namespace Catalog.Api.Contracts
{
    public interface IWarehouseClient
    {
        Task<List<Guid>> GetAvailable(List<Guid> ids);

    }
}
