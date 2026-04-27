using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface IProductsRepository
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetProducts(bool loadCategory = false);
        Product? GetProductById(int productId, bool loadCategory = false);
        void UpdateProduct(int productId, Product product);
        void DeleteProduct(int productId);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}
