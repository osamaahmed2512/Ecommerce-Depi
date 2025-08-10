using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ecommerce.domain.Specification.Categories
{
    public class CategoryByNameSpecification:BaseSpecification<Category>
    {
        public CategoryByNameSpecification(string name)
        : base(c => c.Name.ToString().ToLower().Contains(name))
        {
        }
    }

}
