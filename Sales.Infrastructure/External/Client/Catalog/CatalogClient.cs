using Sales.Domain.Common;
using Sales.Domain.External;

namespace Sales.Infrastructure.External.Client.Catalog
{
    public class CatalogClient : ICatalogClient
    {
        private readonly CatalogServices.CatalogServicesClient catalogServices;

        public CatalogClient(CatalogServices.CatalogServicesClient catalogServices)
        {
            this.catalogServices = catalogServices;
        }

        public async Task<bool> IsProductItemValidAsync(ProductItemId itemId)
        {
            var resp = await catalogServices.ProductExistsAsync(new ProductExistsRequest()
            {
                Id = itemId.Value
            });
            return resp.Exists;
        }
    }


}
