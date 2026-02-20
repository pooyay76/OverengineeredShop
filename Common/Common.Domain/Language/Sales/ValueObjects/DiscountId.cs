using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public record DiscountId : StronglyTypedId
    {
        public DiscountId(Guid value) : base(value)
        {
        }
    }
}