namespace Sales.Api.Contracts.ShoppingCartContracts.Commands
{
    public class DecrementProductItemQuantityCommand
    {
        public Guid UserId { get; set; }
        public long ProductItemId { get; set; }
        public long Quantity { get; set; }

    }
}
