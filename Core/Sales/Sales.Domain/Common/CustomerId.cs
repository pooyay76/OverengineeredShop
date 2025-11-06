using Sales.Domain.Common.Base;

namespace Sales.Domain.Common
{
    public record CustomerId : StronglyTypedId
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}
