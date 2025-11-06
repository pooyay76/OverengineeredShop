namespace Catalog.Api.Exceptions
{
    [Serializable]
    internal class CannotPublishProductWithoutItemsException : ExceptionBase
    {

        private static readonly string DefaultMessage = "Cannot publish product without items exception";

        public CannotPublishProductWithoutItemsException() : base(DefaultMessage)
        {
        }


    }
}