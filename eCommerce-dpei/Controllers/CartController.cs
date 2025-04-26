using AutoMapper;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using eCommerce_dpei.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                
                var product = await _unitOfWork.Products.GetByIdasync(dto.ProductId);
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found" });
                }

                if (!product.IsActive)
                {
                    return BadRequest(new { Message = "Product is not available" });
                }

                if (product.Stock < dto.Quantity)
                {
                    return BadRequest(new { Message = $"Not enough stock available. Current stock: {product.Stock}" });
                }

                var existingCartItem = await _unitOfWork.Cart
                    .FirstOrDefaultAsync(c => c.CustomerId == userId && c.ProductId == dto.ProductId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += dto.Quantity;
                    existingCartItem.UpdatedAt = DateTime.Now;
                    _unitOfWork.Cart.Update(existingCartItem);
                }
                else
                {

                    var CartItem = _mapper.Map<Cart>(dto);
                    CartItem.CustomerId = userId;
                    await _unitOfWork.Cart.AddAsync(CartItem);
                }

                 _unitOfWork.Complete();
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
                var cartItems = await _unitOfWork.Cart
                    .FindAsync(c => c.CustomerId == userId);

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving cart: {ex.Message}" });
            }
        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId,[FromBody] CartUpdateDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var product = await _unitOfWork.Products.GetByIdasync(productId);
                var cartItem = await _unitOfWork.Cart
                    .FirstOrDefaultAsync(c => c.CustomerId == userId && c.ProductId == productId);

                if (cartItem == null)
                {
                    return NotFound(new { Message = "Cart item not found" });
                }

                _mapper.Map(dto, cartItem);
                if (cartItem.Quantity>product.Stock)
                    return BadRequest(new { Message = $"Not enough stock available. Current stock: {product.Stock}" });
                _unitOfWork.Cart.Update(cartItem);
                 _unitOfWork.Complete();

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
                var cartItem = await _unitOfWork.Cart
                    .FirstOrDefaultAsync(c => c.CustomerId == userId && c.ProductId == productId);

                if (cartItem == null)
                {
                    return NotFound(new { Message = "Cart item not found" });
                }

                _unitOfWork.Cart.Remove(cartItem);
                 _unitOfWork.Complete();

                return Ok(new { Message = "Item removed from cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error removing item from cart: {ex.Message}" });
            }
        }
    }
}
