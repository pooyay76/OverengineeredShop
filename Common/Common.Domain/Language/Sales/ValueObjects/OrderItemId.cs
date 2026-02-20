using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public sealed record OrderItemId : StronglyTypedId
    {
        public OrderItemId(Guid value) : base(value)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }


    }
}
