using Shopping.Api.Repositories;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<Cart> GetAll()
        {
            return _cartRepository.GetAll();
        }

        public Cart CreateCart(int productId, int quantity)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) throw new ArgumentException("Product was not found.");
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");

            var cart = new Cart();
            _cartRepository.Add(cart);
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            _cartRepository.Save();
            return cart;
        }

        public Cart GetById(int cartId)
        {
            return _cartRepository.GetById(cartId);
        }

        public void AddProduct(int cartId, int productId, int quantity)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart == null)
            {
                cart = new Cart();
                _cartRepository.Add(cart);
                _cartRepository.Save();
            }

            var product = _productRepository.GetById(productId);
            if (product == null) throw new ArgumentException("Product not found.");
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0.");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }

            _cartRepository.Save();
        }

        public void RemoveProduct(int cartId, int productId)
        {
            var cart = GetById(cartId);
            if (cart == null) throw new ArgumentException("Cart not found.");

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                _cartRepository.Save();
            }
        }

        public void DeleteCart(int cartId)
        {
            var cart = GetById(cartId);
            if (cart == null) throw new ArgumentException("Cart not found.");
            cart.Items.Clear();
            _cartRepository.Delete(cart);
            _cartRepository.Save();
        }
    }
}
