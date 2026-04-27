using Microsoft.AspNetCore.Mvc;
using GroceryModel;
using GroceryViews.ViewModels;
using GroceryService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GroceryViews.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IProductsService _productsService;
        private readonly ITransactionsService _transactionsService;
        private readonly IPaymentsService _paymentsService;
        private readonly IInventoryService _inventoryService;

        public SalesController(ICategoriesService categoriesService, 
                               IProductsService productsService, 
                               ITransactionsService transactionsService,
                               IPaymentsService paymentsService,
                               IInventoryService inventoryService)
        {
            _categoriesService = categoriesService;
            _productsService = productsService;
            _transactionsService = transactionsService;
            _paymentsService = paymentsService;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = _categoriesService.GetCategories()
            };
            return View(salesViewModel);
        }

        public IActionResult SellProductPartial(int productId)
        {
            var product = _productsService.GetProductById(productId);
            return PartialView("_SellProduct", product);
        }

        [HttpPost]
        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                var prod = _productsService.GetProductById(salesViewModel.SelectedProductId);
                if (prod != null)
                {
                    int transactionId = _transactionsService.Add(
                        User.Identity?.Name ?? "Unknown", 
                        salesViewModel.SelectedProductId,
                        prod.Name,
                        prod.UnitPrice,
                        prod.StockQuantity,
                        salesViewModel.QuantityToSell);

                    // Process Payment
                    var payment = new Payment
                    {
                        OrderId = transactionId, // Using transactionId as OrderId for simplicity
                        PaymentMethod = salesViewModel.SelectedPaymentMethod,
                        PaidAmount = prod.UnitPrice * salesViewModel.QuantityToSell,
                        MobileTransactionId = salesViewModel.MobileTransactionId,
                        CardNumber = salesViewModel.CardNumber,
                        PaymentDate = DateTime.Now
                    };
                    
                    _paymentsService.ProcessPayment(payment);
                    
                    bool saleProcessed = _inventoryService.ProcessSale(salesViewModel.SelectedProductId, salesViewModel.QuantityToSell);
                    
                    if (saleProcessed)
                    {
                        TempData["Message"] = $"Payment {payment.Status}! Transaction completed successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "Insufficient stock or error processing sale.";
                    }
                }
            }

            var viewModel = new SalesViewModel
            {
                Categories = _categoriesService.GetCategories(),
                SelectedCategoryId = salesViewModel.SelectedCategoryId,
                SelectedProductId = salesViewModel.SelectedProductId,
                QuantityToSell = salesViewModel.QuantityToSell
            };

            return View("Index", viewModel);
        }
    }
}
