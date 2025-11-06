using Sales.Domain.Common.Base;

namespace Sales.Domain.OrderAgg.Models
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
