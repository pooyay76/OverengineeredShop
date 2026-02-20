using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public sealed record OrderId : StronglyTypedId
    {
        public OrderId(Guid value) : base(value)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }


    }
}
