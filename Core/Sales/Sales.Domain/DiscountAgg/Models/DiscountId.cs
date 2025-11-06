using Sales.Domain.Common.Base;

namespace Sales.Domain.DiscountAgg.Models
{
    public record DiscountId : StronglyTypedId
    {
        public DiscountId(Guid value) : base(value)
        {
        }
    }
}