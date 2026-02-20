namespace Catalog.Api.Exceptions
{
    public class ProductCategoryTitleTooShortException : ExceptionBase
    {
        public static readonly string DefaultMessage = "Product category name cannot be less than 2 characters";
        public ProductCategoryTitleTooShortException() : base(DefaultMessage)
        {
        }

        public ProductCategoryTitleTooShortException(string message) : base(message)
        {
        }
    }
}
