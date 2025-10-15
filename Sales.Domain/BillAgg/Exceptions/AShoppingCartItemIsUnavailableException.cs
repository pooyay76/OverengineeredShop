
using Sales.Domain._common.Base;

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
