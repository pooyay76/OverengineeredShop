using Warehouse.Domain.Common.Base;

namespace Warehouse.Domain.Models
{
    public record InventoryId : StronglyTypedId
    {
        public InventoryId(Guid value) : base(value)
        {
        }
    }
}
