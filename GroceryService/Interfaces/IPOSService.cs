using GroceryModel;

namespace GroceryService.Interfaces
{
    public interface IPOSService
    {
        Order CalculateOrder(List<OrderDetails> items, decimal discount, DiscountType discountType);
        int Checkout(Order order);
        Order? GetOrderById(int orderId);
    }
}
