using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        Order CreateOrder(int cartId);
        Order GetOrder(int orderId);
        IEnumerable<Order> GetOrdersByStatus(OrderStatus status);
        void ChangeOrderStatus(int orderId, OrderStatus status);
    }
}
