using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;

namespace Sales.Api.Contracts.ProductItemPriceContracts.Queries
{
    public class GetLatestProductItemPriceQueryResponse
    {
        public ProductItemId ProductItemId { get; set; }
        public Money Price { get; set; }
    }
}
