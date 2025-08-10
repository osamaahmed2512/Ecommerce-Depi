using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class PayPalCreateOrderDto
    {
        public int OrderId { get; set; }        
        public decimal Amount { get; set; }       
        public string Currency { get; set; } = "USD";
        public string BaseReturnUrl { get; set; }
    }
}
