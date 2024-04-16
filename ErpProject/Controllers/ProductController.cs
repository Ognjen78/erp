using AutoMapper;
using ErpProject.DTO;
using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Helpers;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("api/sportbasic/products")]
    [EnableCors("AllowOrigin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper) 
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
       
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var res = productRepository.getAllProducts();
            if ( res  == null || res.Count == 0)
            {
                return NoContent();
            }
            return Ok(res);        
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Product> GetProductById(int id) 
        {
            Product product = productRepository.getProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Product> CreateProduct([FromBody]Product product)
        {
            try
            {
                Product confirm = productRepository.addProduct(product);
                return Ok(mapper.Map<ProductConfirmDto>(product));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> UpdateProduct(Product product)
        {
            try
            {
                if(productRepository.getProductById(product.id_product) == null)
                {
                    return NotFound("Enter valid ID");
                }

                productRepository.updateProduct(product);   
                return Ok(product);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult RemoveProduct(int id)
        {
            try
            {
                Product product = productRepository.getProductById(id);
                if(product == null)
                {
                    return NotFound("Product with this ID doenst exists");
                }
                productRepository.deleteProduct(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }



    }
}
