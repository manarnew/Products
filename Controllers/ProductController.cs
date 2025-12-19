using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;
using Products.Models.Entities;
namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = dbContext.Products.ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDto addProductDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product
            );
        }


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] ProductDto productDto)
        {
            var existingProduct = dbContext.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;

            dbContext.SaveChanges();

            return Ok(existingProduct);
        }

    }

}