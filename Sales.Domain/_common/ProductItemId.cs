

using Sales.Domain._common.Base;

namespace Sales.Domain._common
{
    public class ProductItemId : StronglyTypedId
    {
        public ProductItemId(Guid value) : base(value)
        {
        }
    }

}
