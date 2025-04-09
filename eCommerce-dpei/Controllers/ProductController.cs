using eCommerce_dpei.Data;
using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ProductController(EcommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error getting products: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }
                return Ok(product); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error getting product: " + ex.Message + " | Inner: " + ex.InnerException?.Message });

            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Returns validation errors
                }

                // Check if CategoryId exists
                if (!_context.Categories.Any(c => c.Id == dto.CategoryId))
                {
                    return BadRequest(new { Message = "Invalid CategoryId: Category does not exist" });
                }

                var product = new Product
                {
                    CategoryId = dto.CategoryId,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = true
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Product created successfully", ProductId = product.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error creating product: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                if (!_context.Categories.Any(c => c.Id == dto.CategoryId))
                {
                    return BadRequest(new { Message = "Invalid CategoryId: Category does not exist" });
                }

                product.CategoryId = dto.CategoryId;
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.Stock = dto.Stock;
                product.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok(new { Message = "Product updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error updating product: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Product deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error deleting product: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }
    }
}