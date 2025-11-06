namespace Sales.Api.Contracts.ShoppingCartContracts.Commands
{
    public class RemoveProductItemCommand
    {
        public long ProductItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
