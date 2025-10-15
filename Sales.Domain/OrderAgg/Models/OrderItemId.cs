using Sales.Domain._common.Base;

namespace Sales.Domain.OrderAgg.Models
{
    public sealed class OrderItemId : StronglyTypedId
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
