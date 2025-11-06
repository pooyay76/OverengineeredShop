using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Api.Contracts.ProductItemPriceContracts.Commands
{
    public class AddProductItemPriceCommand
    {
        public ProductItemId ProductItemId { get; set; }
        public Money Price { get; set; }

    }
}
