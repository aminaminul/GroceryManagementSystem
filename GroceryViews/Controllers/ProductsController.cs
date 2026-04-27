using Microsoft.AspNetCore.Mvc;
using GroceryModel;
using GroceryService.Interfaces;
using GroceryViews.ViewModels;

using Microsoft.AspNetCore.Authorization;

namespace GroceryViews.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        public IActionResult Index() 
        {
            var products = _productsService.GetProducts(loadCategory: true);
            return View(products);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";
            var productViewModel = new ProductViewModel
            {
                Categories = _categoriesService.GetCategories()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productsService.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "add";
            productViewModel.Categories = _categoriesService.GetCategories();
            return View(productViewModel);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel
            {
                Product = _productsService.GetProductById(id) ?? new Product(),
                Categories = _categoriesService.GetCategories()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productsService.UpdateProduct(productViewModel.Product.Id, productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "edit";
            productViewModel.Categories = _categoriesService.GetCategories();
            return View(productViewModel);
        }

        public IActionResult Delete(int id) 
        {
            _productsService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = _productsService.GetProductsByCategoryId(categoryId);
            return PartialView("_Products", products);
        }
    }
}
