using Microsoft.AspNetCore.Mvc;

namespace GroceryViews.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
