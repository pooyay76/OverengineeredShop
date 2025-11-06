
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common.Base;
using Warehouse.Domain.Common.ValueObjects;

namespace Warehouse.Domain.Models
{
    public class WarehouseLog : AggregateRoot<WarehouseLogId>
    {
        public BillId BillId { get; set; }
        public ProductItemId ProductItemId { get; init; }
        public UserId CustomerId { get; init; }
        public bool IsReduction { get; init; }
        public int Quantity { get; init; }
        public string Description { get; init; }
        public UserId OperatorId { get; init; }
        public string OperatorName { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        internal WarehouseLog(ProductItemId productItemId,int quantity, bool isReduction,UserId operatorId ,string operatorName, string description="")
        {

            if(isReduction && (string.IsNullOrWhiteSpace(description) || description.Length==1))
            {
                throw new ArgumentException("Description is required for reduction logs");
            }
            ProductItemId = productItemId;
            IsReduction = isReduction;
            if (quantity == 0)
            {
                throw new ArgumentException("Quantity can't be zero");
            }
            Quantity = quantity;
            Description = description;
            OperatorName = operatorName;
            OperatorId = operatorId;
        }
        internal WarehouseLog(UserId customerId,BillId billId,ProductItemId productItemId, int quantity)
        {
            CustomerId = customerId;
            ProductItemId = productItemId;
            IsReduction = true;
            if (quantity == 0)
            {
                throw new ArgumentException("Quantity can't be zero");
            }
            Quantity = quantity;
            Description = "";
            OperatorName = "System";
            OperatorId = new UserId(Guid.Empty);
        }
    }
}
