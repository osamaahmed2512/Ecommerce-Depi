using Ecommerce.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePayPalOrderAsync(decimal amount, string currency, string returnUrl, string cancelUrl);
        Task<DefaultserviceResponse> CaptureAndUpdateOrderAsync(string paypalToken, int orderId);
        Task<DefaultserviceResponse> CancelOrderPaymentAsync(int orderId);
    }
}
