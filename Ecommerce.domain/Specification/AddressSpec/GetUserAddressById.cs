using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.domain.Specification.AddressSpec
{
    public class GetUserAddressById:BaseSpecification<Address>
    {
        public GetUserAddressById(string userId)
            :base(c =>c.CustomerId==userId)
        {
            
        }
    }
}
