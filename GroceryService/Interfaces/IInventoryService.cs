using GroceryModel;

namespace GroceryService.Interfaces
{
    public interface IInventoryService
    {
        IEnumerable<Inventory> GetAllInventory();
        IEnumerable<Inventory> GetLowStockAlerts();
        IEnumerable<Batch> GetProductBatches(int productId);
        IEnumerable<InventoryLog> GetProductLogs(int productId);
        
        bool ProcessSale(int productId, int quantity);
        void ProcessPurchase(int productId, Batch batch);
        void AdjustStock(int productId, int quantityChange, string remarks, int? batchId = null);
    }
}
