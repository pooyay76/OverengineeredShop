using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Sales.Domain.ShoppingCartAgg.Models;

public class ShoppingCartItem : EntityBase<UserId>
{
    public ShoppingCart ShoppingCart { get; set; }
    public ProductItemId ProductItemId { get; set; }
    public int Quantity { get; set; }

}

