using GroceryModel;

namespace GroceryService.Interfaces
{
    public interface IPaymentsService
    {
        void ProcessPayment(Payment payment);
        IEnumerable<Payment> GetOrderPayments(int orderId);
        void UpdateStatus(int paymentId, TransactionStatus status);
    }
}
