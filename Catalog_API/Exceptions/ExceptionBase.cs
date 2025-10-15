namespace Catalog_API.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public ExceptionBase(string message) : base(message)
        {
        }
    }
}
