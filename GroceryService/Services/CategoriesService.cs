using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public void AddCategory(Category category)
        {
            _categoriesRepository.AddCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoriesRepository.DeleteCategory(categoryId);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoriesRepository.GetCategories();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _categoriesRepository.GetCategoryById(categoryId);
        }

        public void UpdateCategory(int categoryId, Category category)
        {
            _categoriesRepository.UpdateCategory(categoryId, category);
        }
    }
}
