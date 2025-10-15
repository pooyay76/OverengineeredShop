using Sales.Domain._common;
using Sales.Domain._common.Base;
using Sales.Domain.ShoppingCartAgg.Models;

public class ShoppingCartItem : Entity<ShoppingCartItemId>
{
    public ShoppingCart ShoppingCart { get; set; }
    public CustomerId CustomerId { get; set; }
    public ProductItemId ProductItemId { get; set; }
    public int Quantity { get; set; }

}

