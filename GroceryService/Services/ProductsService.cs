using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public void AddProduct(Product product)
        {
            _productsRepository.AddProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            _productsRepository.DeleteProduct(productId);
        }

        public Product? GetProductById(int productId, bool loadCategory = false)
        {
            return _productsRepository.GetProductById(productId, loadCategory);
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false)
        {
            return _productsRepository.GetProducts(loadCategory);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productsRepository.GetProductsByCategoryId(categoryId);
        }

        public void UpdateProduct(int productId, Product product)
        {
            _productsRepository.UpdateProduct(productId, product);
        }
    }
}
