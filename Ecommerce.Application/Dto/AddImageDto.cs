using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class AddImageDto
    {
        public IFormFile Image { get; set; }
        public bool IsPrimary { get; set; } = false;
    }
}
