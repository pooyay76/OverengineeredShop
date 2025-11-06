namespace Sales.Domain.Common
{
    public class ProductItemId
    {
        public long Value { get; set; }
        public ProductItemId(long value)
        {
            Value = value;
        }
    }

}
