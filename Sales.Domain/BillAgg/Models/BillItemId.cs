

using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public class BillItemId : StronglyTypedId
    {
        public BillItemId(Guid value) : base(value)
        {
        }
    }
}
