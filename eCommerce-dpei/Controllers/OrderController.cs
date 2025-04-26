using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using eCommerce_dpei.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var orders = await _unitOfWork.Order
                    .FindAsync(o => o.CustomerId == userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving orders: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var order = await _unitOfWork.Order
                    .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

                if (order == null)
                {
                    return NotFound(new { Message = "Order not found" });
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving order: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

               
                var address = await _unitOfWork.CustomerAddress
                    .FirstOrDefaultAsync(a => a.Id == dto.AddressId && a.CustomerId == userId);
                if (address == null)
                {
                    return BadRequest(new { Message = "Invalid address" });
                }

                // Get cart items
                var cartItems = await _unitOfWork.Cart
                    .FindAsync(c => c.CustomerId == userId);

                if (!cartItems.Any())
                {
                    return BadRequest(new { Message = "Cart is empty" });
                }

                // Calculate total price
                decimal totalPrice = 0;
                foreach (var cartItem in cartItems)
                {
                    var product = await _unitOfWork.Products.GetByIdasync(cartItem.ProductId);
                    if (product == null)
                    {
                        return BadRequest(new { Message = $"Product with ID {cartItem.ProductId} not found" });
                    }
                    totalPrice += product.Price * cartItem.Quantity;
                }

                // Create order
                var order = new Order
                {
                    CustomerId = userId,
                    AddressId = dto.AddressId,
                    TotalPrice = totalPrice,
                    OrderDate = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = "pending"
                };

                await _unitOfWork.Order.AddAsync(order);
                 _unitOfWork.Complete(); // Save to get the order ID

                // Create order items
                foreach (var cartItem in cartItems)
                {
                    var product = await _unitOfWork.Products.GetByIdasync(cartItem.ProductId);

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = product.Price
                    };
                    await _unitOfWork.OrderItem.AddAsync(orderItem);

                    
                    product.Stock -= cartItem.Quantity;
                    _unitOfWork.Products.Update(product);
                }

                // Remove cart items
                foreach (var cartItem in cartItems)
                {
                    _unitOfWork.Cart.Remove(cartItem);
                }

                 _unitOfWork.Complete();

                return Ok(new { Message = "Order created successfully", OrderId = order.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error creating order: {ex.Message}" });
            }
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var order = await _unitOfWork.Order
                    .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

                if (order == null)
                {
                    return NotFound(new { Message = "Order not found" });
                }

                if (order.Status != "pending")
                {
                    return BadRequest(new { Message = "Order cannot be cancelled" });
                }

                order.Status = "cancelled";
                order.UpdatedAt = DateTime.Now;
                _unitOfWork.Order.Update(order);

                // Get order items to restore stoc
                var orderItems = await _unitOfWork.OrderItem
                    .FindAsync(oi => oi.OrderId == id);

                foreach (var item in orderItems)
                {
                    var product = await _unitOfWork.Products.GetByIdasync(item.ProductId);
                    if (product != null)
                    {
                        product.Stock += item.Quantity;
                        _unitOfWork.Products.Update(product);
                    }
                }

                 _unitOfWork.Complete();
                return Ok(new { Message = "Order cancelled successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error cancelling order: {ex.Message}" });
            }
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] string status = null)
        {
            try
            {
                var orders = string.IsNullOrEmpty(status)
                    ? await _unitOfWork.Order.GetAllAsync()
                    : await _unitOfWork.Order.FindAsync(o => o.Status == status);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving orders: {ex.Message}" });
            }
        }

        [HttpPut("admin/{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto dto)
        {
            try
            {
                var order = await _unitOfWork.Order.GetByIdasync(id);
                if (order == null)
                {
                    return NotFound(new { Message = "Order not found" });
                }

                order.Status = dto.Status;
                order.UpdatedAt = DateTime.Now;
                if (!string.IsNullOrEmpty(dto.TrackingNumber))
                {
                    order.ShippingTrackingNumber = dto.TrackingNumber;
                }

                _unitOfWork.Order.Update(order);
                 _unitOfWork.Complete();

                return Ok(new { Message = "Order status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error updating order status: {ex.Message}" });
            }
        }
    }
}
