using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public record BillItemId : StronglyTypedId
    {
        public BillItemId(Guid value) : base(value)
        {
        }
    }
}
