using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Repositories.Models;
using Shopping.Api.Services;

namespace Shopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }
        
        [HttpPost("{cartId}")]
        public ActionResult<Order> CreateOrder(int cartId)
        {
            var order = _orderService.CreateOrder(cartId);
            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            if (order == null) return NotFound("Cart was not found");
            return Ok(order);
        }

        [HttpGet("status/{status}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status)
        {
            var orders = _orderService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpPatch("{orderId}/status/{status}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, OrderStatus status)
        {
            _orderService.ChangeOrderStatus(orderId, status);
            return Ok("Order status updated");
        }
    }
}
