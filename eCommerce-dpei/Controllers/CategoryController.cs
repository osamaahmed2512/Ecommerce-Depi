using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using eCommerce_dpei.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllCategories()
        {
            var categories =await _service.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {   
            var category =await _service.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            await _service.Add(dto);
            return Ok(new { Message = "Category created successfully" });
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
          await  _service.Update(id, dto);
            return Ok(new { Message = "Category Updated successfully" });

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _service.Delete(id);
            return Ok(new { Message = "Category Deleted successfully" });
        }
    }
}