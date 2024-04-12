using AutoMapper;
using ErpProject.DTO;
using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("/api/sportbasic/orders")]
    [EnableCors("AllowOrigin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
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
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            try
            {
                orderRepository.addOrder(order);
                return Ok(mapper.Map<OrderConfirmDto>(order));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Post Error");
            }
            
        }

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
