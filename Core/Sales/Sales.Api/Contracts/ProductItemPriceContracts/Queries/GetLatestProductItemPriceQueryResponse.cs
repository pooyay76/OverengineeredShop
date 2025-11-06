using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Api.Contracts.ProductItemPriceContracts.Queries
{
    public class GetLatestProductItemPriceQueryResponse
    {
        public ProductItemId ProductItemId { get; set; }
        public Money Price { get; set; }
    }
}
