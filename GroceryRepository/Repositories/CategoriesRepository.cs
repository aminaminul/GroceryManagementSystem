using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly GroceryDbContext _context;

        public CategoriesRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetCategories() => _context.Categories.ToList();

        public Category? GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.Id) return;

            var categoryToUpdate = _context.Categories.Find(categoryId);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                categoryToUpdate.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
