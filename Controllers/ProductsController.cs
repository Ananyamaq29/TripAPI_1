using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using System.Collections.Generic;
using System.Linq;
 
namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // In-memory database to hold products temporarily
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Description = "Description for product 1", Price = 10.99M },
            new Product { Id = 2, Name = "Product2", Description = "Description for product 2", Price = 25.99M }
        };
 
        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(products);  // Return the list of all products
        }
 
        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();  // Return 404 if product not found
            }
            return Ok(product);  // Return the found product
        }
 
        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            if (product == null || product.Price < 0)
            {
                return BadRequest("Invalid product data");  // Return 400 if product data is invalid
            }
 
            // Assign a new Id for the product
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);  // Return 201 with the location of the created product
        }
 
        // PUT: api/products/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();  // Return 404 if product not found
            }
 
            if (updatedProduct == null || updatedProduct.Price < 0)
            {
                return BadRequest("Invalid product data");  // Return 400 if the updated product data is invalid
            }
 
            // Update product properties
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
 
            return NoContent();  // Return 204 to indicate that the product was successfully updated
        }
 
        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();  // Return 404 if product not found
            }
 
            products.Remove(product);  // Remove product from the list
            return NoContent();  // Return 204 to indicate successful deletion
        }
    }
}