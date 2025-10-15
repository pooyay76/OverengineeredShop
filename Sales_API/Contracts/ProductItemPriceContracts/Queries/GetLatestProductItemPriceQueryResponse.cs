using Sales.Domain._common.ValueObjects;

namespace Sales.Api.Contracts.ProductItemPriceContracts.Queries
{
    public class GetLatestProductItemPriceQueryResponse
    {
        public Guid ProductItemId { get; set; }
        public Money Price { get; set; }
    }
}
