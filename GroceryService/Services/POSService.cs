using GroceryModel;
using GroceryRepository.Interfaces;
using GroceryService.Interfaces;

namespace GroceryService.Services
{
    public class POSService : IPOSService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IProductsRepository _productsRepository;

        public POSService(IOrdersRepository ordersRepository, IInventoryService inventoryService, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _inventoryService = inventoryService;
            _productsRepository = productsRepository;
        }

        public Order CalculateOrder(List<OrderDetails> items, decimal discount, DiscountType discountType)
        {
            var order = new Order();
            decimal total = 0;

            foreach (var item in items)
            {
                var product = _productsRepository.GetProductById(item.ProductId);
                if (product != null)
                {
                    item.UnitPrice = product.UnitPrice;
                    item.TotalPrice = item.UnitPrice * item.Quantity;
                    total += item.TotalPrice;
                }
            }

            order.OrderDetails = items;
            order.TotalAmount = total;
            order.DiscountType = discountType;
            
            if (discountType == DiscountType.Percentage)
            {
                order.Discount = order.TotalAmount * (discount / 100);
            }
            else
            {
                order.Discount = discount;
            }

            order.FinalAmount = order.TotalAmount - order.Discount;
            
            return order;
        }

        public int Checkout(Order order)
        {
            // Re-calculate to ensure integrity
            var calculatedOrder = CalculateOrder(order.OrderDetails.ToList(), order.Discount, order.DiscountType);
            order.TotalAmount = calculatedOrder.TotalAmount;
            order.Discount = calculatedOrder.Discount;
            order.FinalAmount = calculatedOrder.FinalAmount;

            // Process stock deduction
            foreach (var detail in order.OrderDetails)
            {
                _inventoryService.ProcessSale(detail.ProductId, detail.Quantity);
            }

            order.OrderDate = DateTime.Now;
            return _ordersRepository.AddOrder(order);
        }

        public Order? GetOrderById(int orderId)
        {
            return _ordersRepository.GetOrderById(orderId);
        }
    }
}
