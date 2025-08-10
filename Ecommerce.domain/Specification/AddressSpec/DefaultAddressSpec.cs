



using Ecommerce.domain.entities;

namespace Ecommerce.domain.Specification.AddressSpec
{
    public class DefaultAddressSpec:BaseSpecification<Address>
    {
        public DefaultAddressSpec(string UserId)
            :base(a => a.CustomerId==UserId && a.IsDefault)
        {
            
        }
    }
}
