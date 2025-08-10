using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService Service)
        {
            _service = Service;
        }
        [Authorize(Roles ="User")]
        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result =await _service.GetOrdersByCustomerAsync(userId);
                return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetOrder(int id)
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _service.GetOrderByIdAsync(userId, id);
                if (result == null)
                {
                    return NotFound(new { Message = "Order not found" });
                }

                return Ok(result);
        }
        [Authorize(Roles ="User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromQuery] int AddressId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.CreateOrderAsync(userId, AddressId);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode,new {Message = result.Message});
            }
            return Ok(new {Message = result.Message, orderId=result.Data.Id});
        }

        //[HttpPut("{id}/cancel")]
        //public async Task<IActionResult> CancelOrder(int id)
        //{
        //    try
        //    {
        //        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //        var order = await _unitOfWork.Order
        //            .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

        //        if (order == null)
        //        {
        //            return NotFound(new { Message = "Order not found" });
        //        }

        //        if (order.Status != "pending")
        //        {
        //            return BadRequest(new { Message = "Order cannot be cancelled" });
        //        }

        //        order.Status = "cancelled";
        //        order.UpdatedAt = DateTime.Now;
        //        _unitOfWork.Order.Update(order);

        //        // Get order items to restore stoc
        //        var orderItems = await _unitOfWork.OrderItem
        //            .FindAsync(oi => oi.OrderId == id);

        //        foreach (var item in orderItems)
        //        {
        //            var product = await _unitOfWork.Products.GetByIdasync(item.ProductId);
        //            if (product != null)
        //            {
        //                product.Stock += item.Quantity;
        //                _unitOfWork.Products.Update(product);
        //            }
        //        }

        //         _unitOfWork.Complete();
        //        return Ok(new { Message = "Order cancelled successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = $"Error cancelling order: {ex.Message}" });
        //    }
        //}

        //[HttpGet("admin")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> GetAllOrders([FromQuery] string status = null)
        //{
        //    try
        //    {
        //        var orders = string.IsNullOrEmpty(status)
        //            ? await _unitOfWork.Order.GetAllAsync()
        //            : await _unitOfWork.Order.FindAsync(o => o.Status == status);

        //        return Ok(orders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = $"Error retrieving orders: {ex.Message}" });
        //    }
        //}

        [HttpPut("admin/{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.UpdateOrderStatusAsync(id, status);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode,new {Message= result.Message});
            }
            return Ok(new {Message = result.Message});
        }

    }
}
