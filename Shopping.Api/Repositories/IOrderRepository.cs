using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrdersByStatus(OrderStatus status);
        void ChangeOrderStatus(int orderId, OrderStatus status);
    }
}
