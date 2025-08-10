using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.domain.Specification.products
{
    public class PreductWithimageSpecification:BaseSpecification<Product>
    {
        public PreductWithimageSpecification(int id)
            :base(p =>p.Id==id)
        {
            AddInclude(p => p.ProductImage);
        }
    }
}
