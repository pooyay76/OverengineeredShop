using Common.Domain.Base;

namespace Common.Domain.Language.Sales.ValueObjects
{
    public record BillId : StronglyTypedId
    {
        public BillId(Guid value) : base(value)
        {
        }
    }
}
