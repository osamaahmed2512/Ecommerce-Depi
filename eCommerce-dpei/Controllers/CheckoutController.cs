using AutoMapper;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Filters;
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
    public class CheckoutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICheckoutRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutController(ICheckoutRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("cart-summary")]
        public async Task<IActionResult> GetCartSummary()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var summary = await _repository.GetCartSummary(userId);
                if (summary == null)
                {
                    return NotFound(new { Message = "Cart is empty" });
                }
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving cart summary: {ex.Message}" });
            }
        }

        [HttpGet("shipping-addresses")]
        public async Task<IActionResult> GetShippingAddresses()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var addresses = await _repository.GetUserAddresses(userId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving addresses: {ex.Message}" });
            }
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutProcessDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _repository.ProcessCheckout(dto, userId);

                if (!result.Success)
                {
                    return BadRequest(new { Message = result.Message });
                }

                return Ok(new
                {
                    Message = "Order placed successfully",
                    OrderId = result.OrderId,
                    Status = "pending",
                    Total = result.Total
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error processing checkout: {ex.Message}" });
            }
        }
    }
}
