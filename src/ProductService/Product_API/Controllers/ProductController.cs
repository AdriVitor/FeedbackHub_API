using Microsoft.AspNetCore.Mvc;
using Product_Application.DTOs;
using Product_Application.Services.Interfaces;
using Product_Domain.Entities;

namespace Product_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> Get([FromQuery] int id, [FromQuery] int idEnterprise)
        {
            try
            {
                var product = await _productService.Get(id, idEnterprise);
                return Ok(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{idEnterprise}")]
        public async Task<ActionResult<List<Product>>> GetAll([FromRoute] int idEnterprise)
        {
            try
            {
                var products = await _productService.GetProductsByEnterprise(idEnterprise);
                return Ok(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                await _productService.Add(productDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch([FromBody] ProductDTO productDTO)
        {
            try
            {
                await _productService.Update(productDTO);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int id, [FromQuery] int idEnterprise)
        {
            try
            {
                await _productService.Delete(id, idEnterprise);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
