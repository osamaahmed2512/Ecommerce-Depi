using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Ecommerce.domain.entities
{
    public class TokenModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
