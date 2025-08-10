using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.infrastructure.services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }

        [HttpPost("paypal/create")]
        public async Task<IActionResult> CreatePayPalOrder([FromBody] CreatePayPalOrderRequestDto dto)
        {
            // Verify order exists and belongs to user
            var order = await _orderService.GetOrderByIdAsync(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                dto.OrderId);

            if (order == null)
            {
                return NotFound(new { message = "Order not found" });
            }

            var approvalUrl = await _paymentService.CreatePayPalOrderAsync(
                order.TotalPrice,
                dto.Currency,
                dto.SuccessUrl,
                dto.CancelUrl);

            return Ok(new { approvalUrl, dto.OrderId ,amount = order.TotalPrice});
        }

        [HttpPost("paypal/execute")]
        public async Task<IActionResult> CapturePayPalOrder([FromBody] PayPalCaptureRequestDto dto)
        {
            var result = await _paymentService.CaptureAndUpdateOrderAsync(dto.PayPalToken, dto.OrderId);

            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

        [HttpPost("paypal/cancel")]
        public async Task<IActionResult> CancelPayPalOrder([FromBody] PayPalCancelRequestDto dto)
        {
            var result = await _paymentService.CancelOrderPaymentAsync(dto.OrderId);

            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
    }
}
