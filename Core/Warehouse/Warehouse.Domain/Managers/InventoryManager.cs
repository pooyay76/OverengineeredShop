using Warehouse.Domain.Common.ValueObjects;
using Warehouse.Domain.Contracts;
using Warehouse.Domain.Models;

namespace Warehouse.Domain.Managers
{
    public class InventoryManager
    {
        private readonly IUserServices userServices;
        private readonly IWarehouseLogRepository warehouseLogRepository;
        public InventoryManager(IUserServices userServices, IWarehouseLogRepository warehouseLogRepository)
        {
            this.userServices = userServices;
            this.warehouseLogRepository = warehouseLogRepository;
        }
        public void IncreaseInventory(ProductItemId productItemId,int quantity) 
        {
            var log = new WarehouseLog(productItemId, quantity, false, userServices.GetCurrentUserId(), userServices.GetCurrentUserFullName(), "");
            warehouseLogRepository.AddWarehouseLogAsync(log);

        }
        public void DecreaseInventory(ProductItemId productItemId, int quantity,string description)
        {
            var log = new WarehouseLog(productItemId, quantity, true, userServices.GetCurrentUserId(), userServices.GetCurrentUserFullName(), description);
            warehouseLogRepository.AddWarehouseLogAsync(log);

        }
        public void DecreaseInventoryForPurchase(UserId customerId,BillId billId,List<ProductItemQuantity> purchasedItems)
        {
            List<WarehouseLog> logs = new List<WarehouseLog>();
            foreach (var item in purchasedItems)
            {
                logs.Add(new WarehouseLog(customerId, billId, item.ProductItemId, item.Quantity));
            }
            warehouseLogRepository.AddRangeWarehouseLogAsync(logs);
        }
    }
}
