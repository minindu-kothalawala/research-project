using Microsoft.AspNetCore.Mvc;
using ResearchProject.IServices;
using ResearchProject.Models;

namespace ResearchProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/product
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // POST api/product
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            var created = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        // PUT api/product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest("ID in the URL does not match the body.");

            var updated = _productService.UpdateProduct(product);
            if (!updated)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }

        // DELETE api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _productService.DeleteProduct(id);
            if (!deleted)
                return NotFound($"Product with ID {id} not found.");

            return NoContent();
        }
    }
}
