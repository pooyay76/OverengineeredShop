using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public record ShoppingCartItemId : StronglyTypedId
    {
        public ShoppingCartItemId(Guid value) : base(value)
        {
        }
    }
}
