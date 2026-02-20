using Common.Domain.Language.Catalog.ValueObjects;

namespace Sales.Domain.Contracts
{
    public interface ICatalogClient
    {
        public Task<bool> IsProductItemValidAsync(ProductItemId itemId);

    }
}
