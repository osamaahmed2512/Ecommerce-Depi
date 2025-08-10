using Ecommerce.domain.entities;


namespace Ecommerce.domain.Specification.Cartspec
{
    public class existingCart:BaseSpecification<Cart>
    {
        public existingCart(int productid , string userid)
            :base(c =>c.ProductId==productid && c .UserId==userid)
        {
            
        }

    }
}
