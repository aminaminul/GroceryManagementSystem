using GroceryModel;

namespace GroceryService.Interfaces
{
    public interface ICategoriesService
    {
        void AddCategory(Category category);
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int categoryId);
        void UpdateCategory(int categoryId, Category category);
        void DeleteCategory(int categoryId);
    }
}
