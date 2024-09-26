using Microsoft.EntityFrameworkCore;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Order> GetAll()
        {
            return DbSet.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public override Order GetById(int id)
        {
            return DbSet.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
        {
            return DbSet.Where(o => o.Status == status)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public void ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var order = DbSet.Find(orderId);
            if (order == null) return;
            order.Status = status;
            Context.SaveChanges();
        }
    }
}
