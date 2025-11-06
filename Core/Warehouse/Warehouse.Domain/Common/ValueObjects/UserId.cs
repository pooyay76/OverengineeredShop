using Warehouse.Domain.Common.Base;

namespace Warehouse.Domain.Common.ValueObjects
{
    public record UserId : StronglyTypedId
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}
