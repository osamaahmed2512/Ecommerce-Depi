using System.ComponentModel.DataAnnotations;

namespace eCommerce_dpei.DTOS
{
    public class CartUpdateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
