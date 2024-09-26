using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        Cart CreateCart(int productId, int quantity);
        Cart GetById(int cartId);
        void AddProduct(int cartId, int productId, int quantity);
        void RemoveProduct(int cartId, int productId);
        void DeleteCart(int cartId);
    }
}
