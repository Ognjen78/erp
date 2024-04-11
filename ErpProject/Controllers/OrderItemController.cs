using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("/api/sportbasic/orderItems")]
    [EnableCors("AllowOrigin")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository) 
        {
            this.orderItemRepository = orderItemRepository; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<OrderItem>> GetAllOrderItems()
        {
            var orderItems = orderItemRepository.getAllOrderItems();
            if (orderItems == null || orderItems.Count == 0)
            {
                return NoContent();
            }
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OrderItem> GetOrderItemById(int id)
        {
            OrderItem orderItem = orderItemRepository.getOrderItemById(id);
            if (orderItem == null)
            {
                return NoContent();
            }
            return Ok(orderItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderItem> CreateOrderItem([FromBody] OrderItem orderItem)
        {
           // try
            //{
                
                orderItemRepository.addOrderItem(orderItem);
                return Ok(orderItem);
         //   }
           /* catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert error");
            } */
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                
                orderItemRepository.updateOrderItem(orderItem);
                return Ok(orderItem);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
          
                orderItemRepository.deleteOrderItem(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


    }
}
