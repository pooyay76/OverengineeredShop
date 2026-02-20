using Common.Domain.Language.Catalog.ValueObjects;
using Common.Domain.Language.Global.ValueObjects;
using Common.Domain.Language.Sales.ValueObjects;
using Inventory.Domain.Contracts;
using Inventory.Domain.Models;

namespace Inventory.Domain.Managers
{
    public class InventoryManager
    {
        private readonly IUserServices userServices;
        private readonly IInventoryLogRepository warehouseLogRepository;
        public InventoryManager(IUserServices userServices, IInventoryLogRepository warehouseLogRepository)
        {
            this.userServices = userServices;
            this.warehouseLogRepository = warehouseLogRepository;
        }
        public async Task IncreaseInventoryAsync(ProductItemId productItemId,int quantity) 
        {
            var log = new InventoryLog(productItemId, quantity, false, userServices.GetCurrentUserId(), userServices.GetCurrentUserFullName(), "");
            await warehouseLogRepository.AddWarehouseLogAsync(log);

        }
        public async Task DecreaseInventoryAsync(ProductItemId productItemId, int quantity,string description)
        {
            var log = new InventoryLog(productItemId, quantity, true, userServices.GetCurrentUserId(), userServices.GetCurrentUserFullName(), description);
            await warehouseLogRepository.AddWarehouseLogAsync(log);

        }
        public async Task DecreaseInventoryForPurchaseAsync(UserId customerId,BillId billId,List<ProductItemQuantity> purchasedItems)
        {
            List<InventoryLog> logs = new List<InventoryLog>();
            foreach (var item in purchasedItems)
            {
                logs.Add(new InventoryLog(customerId, billId, item.ProductItemId, item.Quantity));
            }
            await warehouseLogRepository.AddRangeWarehouseLogAsync(logs);
        }
    }
}
