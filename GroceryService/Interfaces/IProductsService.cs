using GroceryModel;

namespace GroceryService.Interfaces
{
    public interface IProductsService
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetProducts(bool loadCategory = false);
        Product? GetProductById(int productId, bool loadCategory = false);
        void UpdateProduct(int productId, Product product);
        void DeleteProduct(int productId);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}
