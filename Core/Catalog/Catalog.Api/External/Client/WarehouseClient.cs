
using Catalog.Api.Contracts.Interfaces;

namespace Catalog.Api.External.Client
{
    public class WarehouseClient : IWarehouseClient
    {
        private readonly WarehouseServices.WarehouseServicesClient client;

        public WarehouseClient(WarehouseServices.WarehouseServicesClient client)
        {
            this.client = client;
        }

        public async Task<List<Guid>> GetAvailable(List<Guid> ids)
        {
           var response =  await  client.GetAvailableProductItemsAsync(new GetAvailableProductItemsRequest
           {
                Ids = { ids.ConvertAll(x => x.ToString()) }
            });
            return response.Ids.Select(Guid.Parse).ToList();
        }
    }
}
