namespace Catalog_API.Exceptions
{
    public class ProductNameTooShortException : ExceptionBase
    {
        private static readonly string DefaultMessage = "Product name cannot be less than 2 characters";
        public ProductNameTooShortException() : base(DefaultMessage)
        {
        }

        public ProductNameTooShortException(string message) : base(message)
        {
        }
    }
}
