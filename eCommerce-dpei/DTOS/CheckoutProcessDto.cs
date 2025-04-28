using System.ComponentModel.DataAnnotations;

namespace eCommerce_dpei.DTOS
{
    public class CheckoutProcessDto
    {
        [Required]
        public int AddressId { get; set; }
    }
}
