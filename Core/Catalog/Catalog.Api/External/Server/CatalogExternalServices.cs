using Catalog.Api.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.External.Server
{
    public class CatalogExternalServices : CatalogServices.CatalogServicesBase
    {
        private readonly CatalogContext catalogContext;

        public CatalogExternalServices(CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        public override async Task<ProductExistsResponse> ProductExists(ProductExistsRequest request, ServerCallContext context)
        {
            var result = new ProductExistsResponse
            {
                Exists = await catalogContext.Products.AnyAsync(x => x.Id == request.Id)
            };
            return result;

        }
    }
}
