using Sales.Domain._common.Base;

namespace Sales.Domain.ShoppingCartAgg.Models
{
    public class ShoppingCartItemId : StronglyTypedId
    {
        public ShoppingCartItemId(Guid value) : base(value)
        {
        }
    }
}
