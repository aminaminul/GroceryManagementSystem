using Microsoft.AspNetCore.Mvc;
using GroceryModel;
using GroceryService.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace GroceryViews.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";
            var category = _categoriesService.GetCategoryById(id.HasValue ? id.Value : 0);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesService.UpdateCategory(category.Id, category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category) 
        {
            if (ModelState.IsValid) 
            {
                _categoriesService.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            return View(category);
        }

        public IActionResult Delete(int id) 
        {
            _categoriesService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
