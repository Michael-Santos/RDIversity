using Microsoft.EntityFrameworkCore;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Repositories
{
    public class CartRepository(DbContext context) : Repository<Cart>(context), ICartRepository
    {
        public override IEnumerable<Cart> GetAll()
        {
            return DbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public override Cart GetById(int id)
        {
            return DbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
