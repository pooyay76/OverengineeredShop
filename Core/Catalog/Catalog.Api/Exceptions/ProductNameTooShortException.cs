namespace Catalog.Api.Exceptions
{
    public class ProductNameTooShortException : ExceptionBase
    {
        public static readonly string DefaultMessage = "Product name cannot be less than 2 characters";
        public ProductNameTooShortException() : base(DefaultMessage)
        {
        }

        public ProductNameTooShortException(string message) : base(message)
        {
        }
    }
}
