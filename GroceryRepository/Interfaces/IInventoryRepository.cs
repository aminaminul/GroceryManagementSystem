using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetAllInventory();
        Inventory? GetInventoryByProductId(int productId);
        void UpdateInventory(Inventory inventory);
        void AddInventory(Inventory inventory);
        
        IEnumerable<Batch> GetBatches(int productId);
        IEnumerable<Batch> GetAvailableBatches(int productId); // Only non-expired and remaining > 0
        void AddBatch(Batch batch);
        void UpdateBatch(Batch batch);
        Batch? GetBatchById(int batchId);
        
        void AddInventoryLog(InventoryLog log);
        IEnumerable<InventoryLog> GetLogsByProduct(int productId);
    }
}
