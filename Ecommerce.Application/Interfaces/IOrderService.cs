using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<DefaultServiceResponseWithData<Order>> CreateOrderAsync(string userId, int AddressId);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
        Task<Order>? GetOrderByIdAsync(string userId, int orderId);
        Task<DefaultserviceResponse> UpdateOrderStatusAsync(int orderId, string status);
    }
}
