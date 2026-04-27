using GroceryModel;
using GroceryRepository.Data;
using GroceryRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly GroceryDbContext _context;

        public InventoryRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inventory> GetAllInventory()
        {
            return _context.Inventories.Include(i => i.Product).ToList();
        }

        public Inventory? GetInventoryByProductId(int productId)
        {
            return _context.Inventories.FirstOrDefault(i => i.ProductId == productId);
        }

        public void UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
        }

        public void AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            _context.SaveChanges();
        }

        public IEnumerable<Batch> GetBatches(int productId)
        {
            return _context.Batches.Where(b => b.ProductId == productId).OrderBy(b => b.ExpiryDate).ToList();
        }

        public IEnumerable<Batch> GetAvailableBatches(int productId)
        {
            return _context.Batches
                .Where(b => b.ProductId == productId && b.RemainingQuantity > 0 && b.ExpiryDate > DateTime.Now)
                .OrderBy(b => b.ExpiryDate)
                .ToList();
        }

        public void AddBatch(Batch batch)
        {
            _context.Batches.Add(batch);
            _context.SaveChanges();
        }

        public void UpdateBatch(Batch batch)
        {
            _context.Batches.Update(batch);
            _context.SaveChanges();
        }

        public Batch? GetBatchById(int batchId)
        {
            return _context.Batches.Find(batchId);
        }

        public void AddInventoryLog(InventoryLog log)
        {
            _context.InventoryLogs.Add(log);
            _context.SaveChanges();
        }

        public IEnumerable<InventoryLog> GetLogsByProduct(int productId)
        {
            return _context.InventoryLogs
                .Where(l => l.ProductId == productId)
                .OrderByDescending(l => l.CreatedAt)
                .ToList();
        }
    }
}
