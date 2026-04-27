using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public IEnumerable<Inventory> GetAllInventory()
        {
            return _inventoryRepository.GetAllInventory();
        }

        public IEnumerable<Inventory> GetLowStockAlerts()
        {
            return _inventoryRepository.GetAllInventory().Where(i => i.IsLowStock);
        }

        public IEnumerable<Batch> GetProductBatches(int productId)
        {
            return _inventoryRepository.GetBatches(productId);
        }

        public IEnumerable<InventoryLog> GetProductLogs(int productId)
        {
            return _inventoryRepository.GetLogsByProduct(productId);
        }

        public bool ProcessSale(int productId, int quantity)
        {
            var inventory = _inventoryRepository.GetInventoryByProductId(productId);
            if (inventory == null || inventory.TotalQuantity < quantity)
                return false;

            var availableBatches = _inventoryRepository.GetAvailableBatches(productId);
            int remainingToDeduct = quantity;

            foreach (var batch in availableBatches)
            {
                if (remainingToDeduct <= 0) break;

                int deduct = Math.Min(batch.RemainingQuantity, remainingToDeduct);
                int oldBatchQuantity = batch.RemainingQuantity;
                batch.RemainingQuantity -= deduct;
                remainingToDeduct -= deduct;

                _inventoryRepository.UpdateBatch(batch);
                
                _inventoryRepository.AddInventoryLog(new InventoryLog
                {
                    ProductId = productId,
                    BatchId = batch.Id,
                    ChangeType = InventoryChangeType.SALE,
                    QuantityChanged = -deduct,
                    PreviousStock = oldBatchQuantity,
                    NewStock = batch.RemainingQuantity,
                    Remarks = $"Sale deduction from batch {batch.BatchNumber}"
                });
            }

            // Fallback for non-batch stock or if batches didn't cover it (shouldn't happen with strict batching)
            if (remainingToDeduct > 0)
            {
                // This would be an error in a strict batch system, but for now we just log it
            }

            int oldTotal = inventory.TotalQuantity;
            inventory.TotalQuantity -= (quantity - remainingToDeduct);
            inventory.LastUpdated = DateTime.Now;
            _inventoryRepository.UpdateInventory(inventory);

            return true;
        }

        public void ProcessPurchase(int productId, Batch batch)
        {
            var inventory = _inventoryRepository.GetInventoryByProductId(productId);
            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductId = productId,
                    TotalQuantity = 0,
                    LowStockThreshold = 10 
                };
                _inventoryRepository.AddInventory(inventory);
            }

            int previousStock = inventory.TotalQuantity;
            batch.ProductId = productId;
            batch.RemainingQuantity = batch.InitialQuantity;
            _inventoryRepository.AddBatch(batch);

            inventory.TotalQuantity += batch.InitialQuantity;
            inventory.LastUpdated = DateTime.Now;
            _inventoryRepository.UpdateInventory(inventory);

            _inventoryRepository.AddInventoryLog(new InventoryLog
            {
                ProductId = productId,
                BatchId = batch.Id,
                ChangeType = InventoryChangeType.PURCHASE,
                QuantityChanged = batch.InitialQuantity,
                PreviousStock = previousStock,
                NewStock = inventory.TotalQuantity,
                Remarks = $"New batch received: {batch.BatchNumber}"
            });
        }

        public void AdjustStock(int productId, int quantityChange, string remarks, int? batchId = null)
        {
            var inventory = _inventoryRepository.GetInventoryByProductId(productId);
            if (inventory == null) return;

            int previousStock = inventory.TotalQuantity;

            if (batchId.HasValue)
            {
                var batch = _inventoryRepository.GetBatchById(batchId.Value);
                if (batch != null)
                {
                    batch.RemainingQuantity += quantityChange;
                    _inventoryRepository.UpdateBatch(batch);
                }
            }

            inventory.TotalQuantity += quantityChange;
            inventory.LastUpdated = DateTime.Now;
            _inventoryRepository.UpdateInventory(inventory);

            _inventoryRepository.AddInventoryLog(new InventoryLog
            {
                ProductId = productId,
                BatchId = batchId,
                ChangeType = InventoryChangeType.ADJUSTMENT,
                QuantityChanged = quantityChange,
                PreviousStock = previousStock,
                NewStock = inventory.TotalQuantity,
                Remarks = remarks
            });
        }
    }
}
