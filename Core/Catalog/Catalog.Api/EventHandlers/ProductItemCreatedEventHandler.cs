using Catalog.Api.Data;
using Common.Domain.Base;
using Common.Domain.Language.Catalog.Events;

namespace Catalog.Api.EventHandlers
{
    public class ProductItemCreatedEventHandler : EventHandlerBase<ProductItemCreatedEvent>
    {
        private readonly CatalogContext _catalogContext;

        public ProductItemCreatedEventHandler(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public override async Task HandleAsync(ProductItemCreatedEvent request)
        {

            var entity = _catalogContext.Products.FirstOrDefault(x => x.Id == request.ProductId);
            entity.Apply(request);
            await _catalogContext.SaveChangesAsync();

        }
    }
}
