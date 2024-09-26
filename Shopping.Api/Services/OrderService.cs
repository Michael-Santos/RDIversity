using Shopping.Api.Repositories;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order CreateOrder(int cartId)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart == null) throw new ArgumentException("Cart not found.");
            if (!cart.Items.Any()) throw new ArgumentException("Cart is empty.");

            var order = new Order();
            foreach (var cartItem in cart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price
                });
            }

            _orderRepository.Add(order);
            _orderRepository.Save();
            _cartRepository.Delete(cart);
            _cartRepository.Save();

            return order;
        }

        public Order GetOrder(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
        {
            return _orderRepository.GetOrdersByStatus(status);
        }

        public void ChangeOrderStatus(int orderId, OrderStatus status)
        {
            _orderRepository.ChangeOrderStatus(orderId, status);
        }
    }
}
