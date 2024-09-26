using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Repositories.Models;
using Shopping.Api.Services;

namespace Shopping.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetAll()
        {
            var carts = _cartService.GetAll();
            return Ok(carts);
        }

        [HttpGet("{cartId}")]
        public ActionResult<Cart> Get(int cartId)
        {
            var cart = _cartService.GetById(cartId);
            if (cart == null) return NotFound("The cart was not found");
            return Ok(cart);
        }

        [HttpPost("{productId}")]
        public IActionResult Post(int productId, [FromQuery] int quantity)
        {
            var result = _cartService.CreateCart(productId, quantity);
            return Ok(result);
        }

        [HttpPatch("{productId}")]
        public IActionResult AddProduct(int cartId, int productId, [FromQuery] int quantity)
        {
            _cartService.AddProduct(cartId, productId, quantity);
            return Ok("Product added to cart");
        }

        [HttpPatch("{cartId}/product/{productId}")]
        public IActionResult RemoveProduct(int cartId, int productId)
        {
            _cartService.RemoveProduct(cartId, productId);
            return Ok("Product removed from cart");
        }

        [HttpDelete("{cartId}")]
        public IActionResult Delete(int cartId)
        {
            _cartService.DeleteCart(cartId);
            return Ok("Cart removed");
        }
    }
}
