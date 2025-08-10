

namespace Ecommerce.Application.Dto
{
    public class CreatePayPalOrderRequestDto
    {
        public int OrderId { get; set; }
        public string Currency { get; set; } = "USD";
        public string SuccessUrl { get; set; }  // URL where PayPal should redirect on success
        public string CancelUrl { get; set; }
    }
}
