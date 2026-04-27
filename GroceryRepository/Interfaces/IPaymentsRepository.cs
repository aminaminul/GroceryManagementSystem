using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface IPaymentsRepository
    {
        void AddPayment(Payment payment);
        IEnumerable<Payment> GetPaymentsByOrderId(int orderId);
        Payment? GetPaymentById(int paymentId);
        void UpdatePaymentStatus(int paymentId, TransactionStatus status);
    }
}
