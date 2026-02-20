using Common.Domain.Base;

namespace Common.Domain.Language.Warehouse.ValueObjects
{
    public record InventoryLogId : StronglyTypedId
    {
        public InventoryLogId() : base()
        {
        }

        public InventoryLogId(Guid value) : base(value)
        {
        }

    }
}
