using GroceryModel;
using GroceryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryViews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IPOSService _posService;

        public BillingController(IPOSService posService)
        {
            _posService = posService;
        }

        /// <summary>
        /// Calculates the order totals based on items and discounts.
        /// </summary>
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] BillingRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
                return BadRequest("Invalid request data.");

            var order = _posService.CalculateOrder(request.Items, request.Discount, request.DiscountType);
            return Ok(new
            {
                Subtotal = order.TotalAmount,
                Discount = order.Discount,
                Total = order.FinalAmount,
                Items = order.OrderDetails
            });
        }

        /// <summary>
        /// Processes the checkout and saves the order.
        /// </summary>
        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] Order order)
        {
            try
            {
                int orderId = _posService.Checkout(order);
                return Ok(new { OrderId = orderId, Message = "Checkout successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Generates a PDF invoice for the given order ID.
        /// </summary>
        [HttpGet("invoice/{id}/pdf")]
        public IActionResult GetInvoicePdf(int id)
        {
            // For now, we return a simple response. 
            // In a real app, this would use a library like QuestPDF to return a FileStreamResult.
            return Ok(new { Message = "PDF Generation triggered for Order #" + id, Action = "Print via /POS/PrintInvoice/" + id });
        }
    }

    public class BillingRequest
    {
        public List<OrderDetails> Items { get; set; } = new();
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
