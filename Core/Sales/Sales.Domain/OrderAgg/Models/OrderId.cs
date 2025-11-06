using Sales.Domain.Common.Base;

namespace Sales.Domain.OrderAgg.Models
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
