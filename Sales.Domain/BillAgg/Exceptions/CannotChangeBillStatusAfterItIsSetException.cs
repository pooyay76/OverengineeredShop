
using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public class CannotChangeBillStatusAfterItIsSetException : DomainException
    {
        private const string _message = "";
        public CannotChangeBillStatusAfterItIsSetException() : base(_message)
        {
        }
    }
}