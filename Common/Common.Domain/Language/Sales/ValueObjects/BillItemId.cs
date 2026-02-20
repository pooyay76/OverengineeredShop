using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public record BillItemId : StronglyTypedId
    {
        public BillItemId(Guid value) : base(value)
        {
        }
    }
}
