using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public PaymentsService(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public void ProcessPayment(Payment payment)
        {
            // Here we would normally integrate with a real payment gateway
            // For now, we simulate success for non-COD payments if TransactionId is provided
            if (payment.PaymentMethod == PaymentMethod.Bkash || payment.PaymentMethod == PaymentMethod.Nagad)
            {
                if (!string.IsNullOrEmpty(payment.MobileTransactionId))
                {
                    payment.Status = TransactionStatus.Success;
                }
            }
            else if (payment.PaymentMethod == PaymentMethod.Card)
            {
                if (!string.IsNullOrEmpty(payment.CardNumber))
                {
                    payment.Status = TransactionStatus.Success;
                }
            }
            else
            {
                payment.Status = TransactionStatus.Pending; // COD is pending until delivered
            }

            _paymentsRepository.AddPayment(payment);
        }

        public IEnumerable<Payment> GetOrderPayments(int orderId)
        {
            return _paymentsRepository.GetPaymentsByOrderId(orderId);
        }

        public void UpdateStatus(int paymentId, TransactionStatus status)
        {
            _paymentsRepository.UpdatePaymentStatus(paymentId, status);
        }
    }
}
