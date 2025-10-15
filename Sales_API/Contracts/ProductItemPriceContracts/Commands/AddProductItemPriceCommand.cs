namespace Sales.Api.Contracts.ProductItemPriceContracts.Commands
{
    public class AddProductItemPriceCommand
    {
        public long ProductItemId { get; set; }
        public long Price { get; set; }

    }
}
