using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface ITransactionsRepository
    {
        IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date);
        IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate);
        int Add(string cashierName, int productId, string productName, decimal price, int beforeQty, int soldQty);
    }
}
