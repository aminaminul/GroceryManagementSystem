using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly GroceryDbContext _context;

        public PaymentsRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public IEnumerable<Payment> GetPaymentsByOrderId(int orderId)
        {
            return _context.Payments.Where(p => p.OrderId == orderId).ToList();
        }

        public Payment? GetPaymentById(int paymentId)
        {
            return _context.Payments.Find(paymentId);
        }

        public void UpdatePaymentStatus(int paymentId, TransactionStatus status)
        {
            var payment = _context.Payments.Find(paymentId);
            if (payment != null)
            {
                payment.Status = status;
                _context.SaveChanges();
            }
        }
    }
}
