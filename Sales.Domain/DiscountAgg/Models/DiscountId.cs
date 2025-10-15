using Sales.Domain._common.Base;

namespace Sales.Domain.DiscountAgg.Models
{
    public class DiscountId : StronglyTypedId
    {
        public DiscountId(Guid value) : base(value)
        {
        }
    }
}