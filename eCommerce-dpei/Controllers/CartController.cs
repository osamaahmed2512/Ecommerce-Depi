using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CartController : ControllerBase
    {

        private readonly IcartService _service;
        public CartController( IcartService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto dto)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.Add(UserId, dto);
            if (!result.Success)
            {
                return result.Type switch
                {
                    "Not Found" => NotFound(result),
                    _ => BadRequest(new { message = result.Message })
                };
            }
            return Ok(new {message = result.Message});
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result =await _service.GetUserCart(userId);
                return Ok(result);


        }
        
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId , UpdateCartDto dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("user not authorized");
            var result = await _service.update(userId,productId, dto);
            if (!result.Success)
            {
                return result.Type switch
                {
                    "Not Found" => NotFound(new { messgae = result.Message }),
                    _ => BadRequest(new { message = result.Message })
                };
            }
            return Ok(new { message = result.Message });
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.Delete(UserId, productId);
            if (!result.Success)
            {
                return result.Type switch
                {
                    "Not Found" => NotFound(new {messgae= result.Message}),
                    _ => BadRequest(new { message = result.Message })
                };
            }
            return Ok(new { message = result.Message });
        }

    }
}
