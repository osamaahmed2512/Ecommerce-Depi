

using Ecommerce.domain.entities;

namespace Ecommerce.domain.Specification.Cartspec
{
    public class CartWithProductSpecification:BaseSpecification<Cart>
        
    {
        public CartWithProductSpecification(string userId)
            :base(c =>c.UserId ==userId)
        {
            AddInclude(c => c.Product);
        }
    }
}
