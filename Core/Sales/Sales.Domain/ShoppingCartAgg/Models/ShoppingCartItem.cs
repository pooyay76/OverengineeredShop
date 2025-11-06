using Sales.Domain.Common;
using Sales.Domain.Common.Base;
using Sales.Domain.ShoppingCartAgg.Models;

public class ShoppingCartItem : Entity<CustomerId>
{
    public ShoppingCart ShoppingCart { get; set; }
    public ProductItemId ProductItemId { get; set; }
    public int Quantity { get; set; }

}

