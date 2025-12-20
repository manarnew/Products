using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Services;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetProducts()
            => Ok(_service.GetAll());

        [HttpGet("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = _service.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto dto)
        {
            var product = _service.Create(dto);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product
            );
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, ProductDto dto)
        {
            var product = _service.Update(id, dto);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var deleted = _service.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
