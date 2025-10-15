namespace Sales.Api.Contracts.ShoppingCartContracts.Commands
{
    public class AddProductItemCommand
    {
        public long ProductItemId { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
    }
}
