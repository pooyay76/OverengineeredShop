namespace Catalog.Api.Exceptions
{
    public class ProductDescriptionTooShortException : ExceptionBase
    {
        public static readonly string DefaultMessage = "Product description cannot be less than 10 characters";
        public ProductDescriptionTooShortException() : base(DefaultMessage)
        {
        }

        public ProductDescriptionTooShortException(string message) : base(message)
        {
        }
    }
}

