using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eCommerce_dpei.DTOS
{
    public class AddProductImageDto
    {
        [Required(ErrorMessage = "productId is requiredd")]
        public int productId {  get; set; }
        [Required(ErrorMessage ="image is requierd")]
        [FromForm]
        public List<IFormFile> images { get; set; }
    }
}
