using eCommerce_dpei.Data;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace eCommerce_dpei.repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly EcommerceContext _context;
        public ProductImageRepository(EcommerceContext context)
        {
            _context = context;
        }
        public async Task Add(int productId, List<IFormFile> images)
        {
            foreach (var image in images)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                var savePath = Path.Combine("wwwroot/images/products", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImageUrl = $"/images/products/{fileName}",
                    IsPrimary = false,
                    CreatedAt = DateTime.Now
                };

                _context.ProductImages.Add(productImage);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<bool> Remove(int imageid)
        {
            var productimage =await _context.ProductImages.FirstOrDefaultAsync(x =>x.Id == imageid);
            if (productimage == null)
                return false;

             _context.ProductImages.Remove(productimage);
            return true;
        }
    }
}
