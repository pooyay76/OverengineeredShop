namespace Catalog.Api.Exceptions
{
    [Serializable]
    internal class InvalidProductPriceException : ExceptionBase
    {
        private static readonly string DefaultMessage = "Product name cannot be less than 2 characters";

        public InvalidProductPriceException() : base(DefaultMessage)
        {
        }


    }
}