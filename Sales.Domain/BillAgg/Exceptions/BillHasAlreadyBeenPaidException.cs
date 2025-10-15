using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class BillHasAlreadyBeenPaidException : DomainException
    {
        private const string _message = "";
        public BillHasAlreadyBeenPaidException() : base(_message)
        {
        }
    }
}