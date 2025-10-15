
using Sales.Domain._common.Base;

namespace Sales.Domain._common
{
    public class CustomerId : StronglyTypedId
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}
