using AutoMapper;
using ErpProject.DTO;
using ErpProject.Helpers;
using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("/api/sportbasic/orders")]
    [EnableCors("AllowOrigin")]
   // [Authorize(Policy = "RequireAdminRole")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly OrderService orderService;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, OrderService orderService)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Order>> GetAllOrders()
        {
            var orders = orderRepository.getAllOrders();
            if (orders == null || orders.Count == 0)
            {
                return NoContent();
            }
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Order> GetOrderById(int id)
        {
            Order order = orderRepository.getOrderById(id);
            if (order == null)
            {
                return NoContent();
            }
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDto request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var order = await orderService.CreateOrder(request);
                return Ok(mapper.Map<OrderConfirmDto>(order));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Post Error: {ex.Message}");
            }
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Order> UpdateOrder(Order order)
        {
            try
            {
                
                orderRepository.updateOrder(order);
                return Ok(order);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                
                orderRepository.deleteOrder(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
