using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class OrderDto
    {
        public int AddressId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
