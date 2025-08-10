using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces.Repository
{
    public interface IProductImageRepository:IGenericRepository<ProductImage>
    {
        Task<List<ProductImage>> GetImagesByProductId(int productId);
    }
}
