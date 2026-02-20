using Common.Application.Base;
using Common.Domain.Language.Enums;
using Common.Domain.Language.Global.ValueObjects;

namespace Sales.Application.BillUseCases.Commands
{
    public class CreateBillCommand : CommandBase
    {
        public ShippingInformation ShippingInformation { get; set; }
        public UserType CustomerType { get; set; }
        public UserId CustomerId { get; set; }
    }
}