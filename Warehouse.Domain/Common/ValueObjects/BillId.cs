using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public record BillId : StronglyTypedId
    {
        public BillId(Guid value) : base(value)
        {
        }
    }
}
