namespace Catalog_API.Exceptions
{
    public class ProductCategoryTitleTooShortException : ExceptionBase
    {
        private static readonly string DefaultMessage = "Product name cannot be less than 2 characters";
        public ProductCategoryTitleTooShortException() : base(DefaultMessage)
        {
        }

        public ProductCategoryTitleTooShortException(string message) : base(message)
        {
        }
    }
}
