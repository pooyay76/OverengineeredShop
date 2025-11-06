namespace Catalog.Api.Events
{
    public class ProductPriceSetEvent
    {
        public Guid Id { get; set; } = new Guid();
        public int MyProperty { get; set; }
    }
}
