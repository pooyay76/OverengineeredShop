using MediatR;
using Sales.Application.DTOs;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;

namespace Sales.Application.BillUseCases.Commands
{
    public class CreateBillCommand : IRequest<CommandResponseGeneric<Bill>>
    {
        public ShippingInformation ShippingInformation { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerId CustomerId { get; set; }
    }
}