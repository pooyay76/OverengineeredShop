
using Sales.Domain._common.Base;

namespace Sales.Domain.BillAgg.Models
{
    public class BillExpiredException : DomainException
    {
        private const string _message = "";
        public BillExpiredException() : base(_message)
        {
        }
    }
}