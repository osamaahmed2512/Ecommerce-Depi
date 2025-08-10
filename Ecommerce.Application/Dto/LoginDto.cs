using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Dto
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}