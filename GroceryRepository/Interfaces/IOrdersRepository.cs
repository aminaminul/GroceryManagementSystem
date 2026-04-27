using GroceryModel;

namespace GroceryRepository.Interfaces
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        Order? GetOrderById(int orderId);
        IEnumerable<Order> GetAllOrders();
    }
}
