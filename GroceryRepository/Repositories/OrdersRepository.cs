using GroceryModel;
using GroceryRepository.Data;
using GroceryRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GroceryRepository.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly GroceryDbContext _context;

        public OrdersRepository(GroceryDbContext context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Customer).ToList();
        }
    }
}
