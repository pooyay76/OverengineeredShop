using Sales.Domain.Common.Base;

namespace Sales.Domain.BillAgg.Exceptions
{
    public class EmptyBillException : DomainException
    {
        private const string _message = "";
        public EmptyBillException() : base(_message)
        {
        }
    }
}
