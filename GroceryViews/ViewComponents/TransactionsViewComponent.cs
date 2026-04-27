using Microsoft.AspNetCore.Mvc;
using GroceryModel;
using GroceryService.Interfaces;

namespace GroceryViews.ViewComponents
{
    [ViewComponent]
    public class TransactionsViewComponent : ViewComponent
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsViewComponent(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        public IViewComponentResult Invoke(string userName)
        {
            var transactions = _transactionsService.GetByDayAndCashier(userName, DateTime.Now);

            return View(transactions);
        }
    }
}
