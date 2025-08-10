using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class PayPalCaptureRequestDto
    {
        public string PayPalToken { get; set; }
        public int OrderId { get; set; }
    }
}
