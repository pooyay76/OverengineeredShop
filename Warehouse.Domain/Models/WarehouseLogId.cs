using Sales.Domain.Common.Base;

namespace Warehouse.Domain.Models
{
    public record WarehouseLogId : StronglyTypedId
    {
        public WarehouseLogId(Guid value) : base(value)
        {
        }
    }
}
