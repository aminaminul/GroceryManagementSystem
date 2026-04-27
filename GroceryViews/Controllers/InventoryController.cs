using GroceryModel;
using GroceryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryViews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_inventoryService.GetAllInventory());
        }

        [HttpGet("low-stock")]
        public IActionResult GetLowStock()
        {
            return Ok(_inventoryService.GetLowStockAlerts());
        }

        [HttpGet("{productId}/batches")]
        public IActionResult GetBatches(int productId)
        {
            return Ok(_inventoryService.GetProductBatches(productId));
        }

        [HttpGet("{productId}/logs")]
        public IActionResult GetLogs(int productId)
        {
            return Ok(_inventoryService.GetProductLogs(productId));
        }

        [HttpPost("adjust")]
        public IActionResult AdjustStock([FromBody] StockAdjustmentRequest request)
        {
            _inventoryService.AdjustStock(request.ProductId, request.QuantityChange, request.Remarks, request.BatchId);
            return Ok(new { message = "Stock adjusted successfully" });
        }
    }

    public class StockAdjustmentRequest
    {
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int? BatchId { get; set; }
    }
}
