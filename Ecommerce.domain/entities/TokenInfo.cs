using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.domain.entities
{
    public class TokenInfo
    {
        public int Id { get; set; }
        [Required,MaxLength(35)]
        public string Username { get; set; }
        [Required, MaxLength(200)]
        public string RefreshToken { get; set; }
        [Required]
        public DateTime ExpiredAt { get; set; }
    }
}
