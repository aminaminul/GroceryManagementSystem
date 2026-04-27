using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface ICategoriesRepository
    {
        void AddCategory(Category category);
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int categoryId);
        void UpdateCategory(int categoryId, Category category);
        void DeleteCategory(int categoryId);
    }
}
