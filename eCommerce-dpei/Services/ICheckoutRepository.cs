using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;

namespace eCommerce_dpei.Services
{
    public interface ICheckoutRepository
    {
        Task<CartSummaryDto> GetCartSummary(int userId);
        Task<List<CustomerAddress>> GetUserAddresses(int userId);
        Task<CheckoutResultDto> ProcessCheckout(CheckoutProcessDto dto, int userId);
    }
}
