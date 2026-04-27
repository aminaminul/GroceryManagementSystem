using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public int Add(string cashierName, int productId, string productName, decimal price, int beforeQty, int soldQty)
        {
            return _transactionsRepository.Add(cashierName, productId, productName, price, beforeQty, soldQty);
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            return _transactionsRepository.GetByDayAndCashier(cashierName, date);
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            return _transactionsRepository.Search(cashierName, startDate, endDate);
        }
    }
}
