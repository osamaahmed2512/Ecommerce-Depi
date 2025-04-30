using eCommerce_dpei.Data;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Filters;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidatorFilter))]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _Repository;
        private readonly EcommerceContext _context;

        public ProductImageController(IProductImageRepository Repository, EcommerceContext context)
        {
            _Repository = Repository;
            _context = context;
        }

        [HttpPost("api/products/{productId}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProductImages(AddProductImageDto Dto)
        {
            var product = await _context.Products.FindAsync(Dto.productId);
            if (product == null)
                return NotFound("Product not found.");

            await  _Repository.Add(Dto.productId,Dto.images);

            return Ok("Images added successfully.");
        }
        [HttpDelete("api/images/{imageid}")]
        public async Task<IActionResult> Delete(int imageid)
        {
            var addImageSuccess =  await _Repository.Remove(imageid);

            if (!addImageSuccess)
            {
                return NotFound("image not found");
            }
            return Ok("Images deleted successfully.");
        }
    }
}
