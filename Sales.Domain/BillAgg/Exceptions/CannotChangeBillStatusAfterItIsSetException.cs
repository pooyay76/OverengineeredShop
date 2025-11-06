using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class CannotChangeBillStatusAfterItIsSetException : DomainException
    {
        private const string _message = "";
        public CannotChangeBillStatusAfterItIsSetException() : base(_message)
        {
        }
    }
}