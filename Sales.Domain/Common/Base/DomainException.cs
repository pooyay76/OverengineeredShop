namespace Sales.Domain.Common.Base
{

    /// <summary>
    /// Sometimes we use this as the parent of a domain exception, 
    /// sometimes we just instantiate it where we need it
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
