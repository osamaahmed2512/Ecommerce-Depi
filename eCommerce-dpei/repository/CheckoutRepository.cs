using AutoMapper;
using eCommerce_dpei.Data;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_dpei.repository
{
    public class CheckoutRepository:ICheckoutRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutRepository(EcommerceContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartSummaryDto> GetCartSummary(int userId)
        {
            var cartItems = await _context.Cart
                .Include(c => c.Product)
                .Where(c => c.CustomerId == userId)
                .ToListAsync();

            if (!cartItems.Any())
                return null;

            var summary = new CartSummaryDto
            {
                Items = cartItems.Select(item => new CartItemSummaryDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Subtotal = item.Product.Price * item.Quantity
                }).ToList(),
                TotalItems = cartItems.Sum(i => i.Quantity),
                TotalAmount = cartItems.Sum(i => i.Product.Price * i.Quantity)
            };

            return summary;
        }

        public async Task<List<CustomerAddress>> GetUserAddresses(int userId)
        {
            return await _context.CustomerAddresses
                .Where(a => a.CustomerId == userId)
                .OrderByDescending(a => a.IsDefault)
                .ToListAsync();
        }

        public async Task<CheckoutResultDto> ProcessCheckout(CheckoutProcessDto dto, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate address
                var address = await _context.CustomerAddresses
                    .FirstOrDefaultAsync(a => a.Id == dto.AddressId && a.CustomerId == userId);

                if (address == null)
                {
                    return new CheckoutResultDto
                    {
                        Success = false,
                        Message = "Invalid shipping address"
                    };
                }

                // Get cart items
                var cartItems = await _context.Cart
                    .Include(c => c.Product)
                    .Where(c => c.CustomerId == userId)
                    .ToListAsync();

                if (!cartItems.Any())
                {
                    return new CheckoutResultDto
                    {
                        Success = false,
                        Message = "Cart is empty"
                    };
                }

                // Validate stock and calculate total
                decimal totalAmount = 0;
                foreach (var item in cartItems)
                {
                    if (item.Product.Stock < item.Quantity)
                    {
                        return new CheckoutResultDto
                        {
                            Success = false,
                            Message = $"Insufficient stock for product: {item.Product.Name}"
                        };
                    }
                    totalAmount += item.Product.Price * item.Quantity;
                }

                // Create order
                var order = new Order
                {
                    CustomerId = userId,
                    AddressId = dto.AddressId,
                    TotalPrice = totalAmount,
                    OrderDate = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = "pending"
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                // Create order items and update stock
                foreach (var item in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };
                    await _context.OrderItems.AddAsync(orderItem);

                    // Update product stock
                    item.Product.Stock -= item.Quantity;
                    _context.Products.Update(item.Product);
                }

                // Clear cart
                _context.Cart.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CheckoutResultDto
                {
                    Success = true,
                    OrderId = order.Id,
                    Total = totalAmount
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
