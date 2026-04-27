using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly GroceryDbContext _context;

        public TransactionsRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return _context.Transactions.Where(x => x.TimeStamp.Date == date.Date).ToList();
            else
                return _context.Transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp.Date == date.Date).ToList();
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return _context.Transactions.Where(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date).ToList();
            else
                return _context.Transactions.Where(x =>
                    x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                    x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date).ToList();
        }

        public int Add(string cashierName, int productId, string productName, decimal price, int beforeQty, int soldQty)
        {
            var transaction = new Transaction
            {
                ProductId = productId,
                ProductName = productName,
                TimeStamp = DateTime.Now,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                CashierName = cashierName
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
            return transaction.TransactionId;
        }
    }
}
