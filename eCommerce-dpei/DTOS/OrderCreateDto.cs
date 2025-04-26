using System.ComponentModel.DataAnnotations;

namespace eCommerce_dpei.DTOS
{
    public class OrderCreateDto
    {
        [Required]
        public int AddressId { get; set; }
    }
}
