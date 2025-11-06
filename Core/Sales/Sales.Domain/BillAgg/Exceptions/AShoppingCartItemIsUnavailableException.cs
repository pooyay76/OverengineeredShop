using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class AShoppingCartItemIsUnavailableException : DomainException
    {
        private const string _message = "";
        public AShoppingCartItemIsUnavailableException() : base(_message)
        {
        }
    }
}
