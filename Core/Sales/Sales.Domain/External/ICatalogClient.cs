using Sales.Domain.Common;

namespace Sales.Domain.External
{
    public interface ICatalogClient
    {
        public Task<bool> IsProductItemValidAsync(ProductItemId itemId);

    }
}
