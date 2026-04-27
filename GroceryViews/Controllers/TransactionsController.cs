using Microsoft.AspNetCore.Mvc;
using GroceryModel;
using GroceryViews.ViewModels;
using GroceryService.Interfaces;

using Microsoft.AspNetCore.Authorization;

namespace GroceryViews.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        public IActionResult Index()
        {
            TransactionsViewModel transactionsViewModel = new TransactionsViewModel();
            return View(transactionsViewModel);
        }

        public IActionResult Search(TransactionsViewModel transactionsViewModel)
        {
            var transactions = _transactionsService.Search(
                transactionsViewModel.CashierName??string.Empty,
                transactionsViewModel.StartDate,
                transactionsViewModel.EndDate);

            transactionsViewModel.Transactions = transactions;
            return View("Index", transactionsViewModel);
        }
    }
}
