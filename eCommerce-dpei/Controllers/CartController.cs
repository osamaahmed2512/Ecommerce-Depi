using AutoMapper;
using eCommerce_dpei.Data;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Filters;
using eCommerce_dpei.Models;
using eCommerce_dpei.repository;
using eCommerce_dpei.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(ValidatorFilter))]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _repository;
        private readonly EcommerceContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CartController(ICartRepository repository, IMapper mapper, EcommerceContext context , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto dto)
        {
            try
            {
                var product = _context.Products.Find(dto.ProductId);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                if (product.Stock < dto.Quantity)
                {
                    return BadRequest(new { Message = $"Not enough stock available. Current stock: {product.Stock}" });
                }
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var existingCartItem = await _repository.Create(dto, userId);
                return Ok(new { Message = "Item added to cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error adding item to cart: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var cartItems = await _repository.Get(c => c.CustomerId == userId);
                if ( cartItems == null)
                {
                    return NotFound(new { Message = "Cart not found" });
                }
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving cart: {ex.Message}" });
            }
        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId, [FromBody] CartUpdateDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


                var cartItem = await _repository.Update(productId, userId, dto);
                if (! cartItem)
                {
                    return NotFound(new { Message = "Cart item not found or product quantity more than staock" });
                }


                return Ok(new { Message = "Cart item updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error updating cart item: {ex.Message}" });
            }
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var cartItem =await _repository.Delete(productId,userId);

                if (cartItem == null)
                {
                    return NotFound(new { Message = "Cart item not found" });
                }

                return Ok(new { Message = "Item removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error removing item from cart: {ex.Message}" });
            }
        }
    }
}
