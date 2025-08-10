using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.percistence.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly ApplcationDbContext _context;
        public ProductImageRepository(ApplcationDbContext Context) : base(Context)
        {
            _context = Context;
        }
        public async Task<List<ProductImage>> GetImagesByProductId(int productId)
        {
            return await _context.ProductImages.Where(img => img.ProductId == productId).ToListAsync();
        }
    }
}
