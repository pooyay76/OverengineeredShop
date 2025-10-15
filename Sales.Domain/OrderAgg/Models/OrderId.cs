using Sales.Domain._common.Base;

namespace Sales.Domain.OrderAgg.Models
{
    public sealed class OrderId : StronglyTypedId
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
