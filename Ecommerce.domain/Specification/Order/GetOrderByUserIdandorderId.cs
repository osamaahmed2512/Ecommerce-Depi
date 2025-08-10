using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.domain.Specification.Orderspec
{
    public class GetOrderByUserIdandorderId:BaseSpecification<Order>
    {
        public GetOrderByUserIdandorderId(string UserId , int OrderId)
            :base(x => x.UserId ==UserId &&x.Id==OrderId)
        {
            
        }
    }
}
