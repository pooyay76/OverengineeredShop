using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public class BillId : StronglyTypedId
    {
        internal BillId(Guid value) : base(value)
        {
        }
    }
}
