

using Ecommerce.domain.entities;

namespace Ecommerce.domain.Specification.Orderspec
{
    public class GetOrderByUserIdspec:BaseSpecification<Order>
    {
        public GetOrderByUserIdspec(string userId)
            :base(c =>c.UserId==userId)
        {
            
        }
    }
}
