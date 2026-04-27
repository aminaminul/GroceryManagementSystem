using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly GroceryDbContext _context;

        public ProductsRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false) 
        {
            if (!loadCategory)
            {
                return _context.Products.ToList();
            }
            else
            {
                return _context.Products.Include(p => p.Category).ToList();
            }
        }

        public Product? GetProductById(int productId, bool loadCategory = false)
        {
            if (loadCategory)
            {
                return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == productId);
            }
            return _context.Products.Find(productId);
        }

        public void UpdateProduct(int productId, Product product)
        {
            if (productId != product.Id) return;

            var productToUpdate = _context.Products.Find(productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.StockQuantity = product.StockQuantity;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.SupplierId = product.SupplierId;
                productToUpdate.Barcode = product.Barcode;
                productToUpdate.ExpiryDate = product.ExpiryDate;
                productToUpdate.Status = product.Status;
                productToUpdate.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
