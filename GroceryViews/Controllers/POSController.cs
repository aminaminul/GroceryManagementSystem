using GroceryModel;
using GroceryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryViews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : ControllerBase
    {
        private readonly IPOSService _posService;
        private readonly IProductsService _productsService;

        public POSController(IPOSService posService, IProductsService productsService)
        {
            _posService = posService;
            _productsService = productsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("PrintInvoice/{id}")]
        public IActionResult PrintInvoice(int id)
        {
            var order = _posService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            return Ok(_productsService.GetProducts());
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] POSCalculationRequest request)
        {
            var order = _posService.CalculateOrder(request.Items, request.Discount, request.DiscountType);
            return Ok(order);
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] Order order)
        {
            int orderId = _posService.Checkout(order);
            return Ok(new { orderId });
        }

        [HttpGet("invoice/{id}")]
        public IActionResult GetInvoice(int id)
        {
            var order = _posService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }

    public class POSCalculationRequest
    {
        public List<OrderDetails> Items { get; set; } = new();
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
