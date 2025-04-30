using eCommerce_dpei.Filters;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [ServiceFilter(typeof(ValidatorFilter))]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _Repository;

        public CategoryController(ICategoryRepository Repository)
        {
            _Repository = Repository;
        }

        [HttpGet]
        public  IActionResult GetAllCategories()
        {
            try
            {
                var categories = _Repository.GetAll();
                if (categories == null)
                {
                    return NotFound("no categories found");
                }
                return Ok(categories); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error getting categories: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var category = _Repository.Get(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found" });
                }
                return Ok(category); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error getting category: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            try
            {
                var category =await _Repository.Create(dto);
                return Ok(new { Message = "Category created successfully", CategoryId = category.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error creating category: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
            try
            {
                var success =await _Repository.Update(id,dto);
                if (!success )
                {
                    return NotFound(new { Message = "Category not found" });
                }

                return Ok(new { Message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error updating category: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category =await _Repository.Delete(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found" });
                }
                return Ok(new { Message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error deleting category: " + ex.Message + " | Inner: " + ex.InnerException?.Message });
            }
        }
    }
}