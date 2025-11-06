using Warehouse.Domain.Common.Base;

namespace Warehouse.Domain.Common.ValueObjects
{
    public record BillId : StronglyTypedId
    {
        public BillId(Guid value) : base(value)
        {
        }
    }
}
