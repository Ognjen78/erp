using AutoMapper;
using ErpProject.DTO;
using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("/api/sportbasic/shippingAddress")]
    [EnableCors("AllowOrigin")]
    public class ShippingController : Controller
    {
        private readonly IShippingRepository shippingRepository;
        private readonly IMapper mapper;

        public ShippingController(IShippingRepository shippingRepository, IMapper mapper)
        {
            this.shippingRepository = shippingRepository;   
            this.mapper = mapper;   
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ShippingAddress>> GetAllShippingAddresses()
        {
            var shippingAddresses = shippingRepository.getAllShippingAddresses();
            if (shippingAddresses == null || shippingAddresses.Count == 0)
            {
                return NoContent();
            }
            return Ok(shippingAddresses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ShippingAddress> GetShippingAddressById(int id)
        {
            ShippingAddress shippingAddress = shippingRepository.getShippingAddressById(id);
            if (shippingAddress == null)
            {
                return NoContent();
            }
            return Ok(shippingAddress);
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShippingAddress> CreateShippingAddress([FromBody] ShippingAddress shippingAddress)
        {
            try
            {

                shippingRepository.addShippingAddress(shippingAddress);
                return Ok(mapper.Map<ShippingConfirmDto>(shippingAddress));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Post shipping error");
            }
           
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShippingAddress> UpdateShippingAddress(ShippingAddress shippingAddress)
        {
            try
            {
               
                shippingRepository.updateShippingAddress(shippingAddress);
                return Ok(shippingAddress);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult DeleteShippingAddress(int id)
        {
            try
            {
               
                shippingRepository.deleteShippingAddress(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
