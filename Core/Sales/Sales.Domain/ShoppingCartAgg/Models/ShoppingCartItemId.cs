using Sales.Domain.Common.Base;

namespace Sales.Domain.ShoppingCartAgg.Models
{
    public record ShoppingCartItemId : StronglyTypedId
    {
        public ShoppingCartItemId(Guid value) : base(value)
        {
        }
    }
}
