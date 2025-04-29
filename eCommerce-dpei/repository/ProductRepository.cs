using eCommerce_dpei.Data;
using eCommerce_dpei.Migrations;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_dpei.repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;
        public ProductRepository(EcommerceContext Context)
        {
            _context = Context;
        }
        public bool isCategoryExist(int Id)
        {
            return _context.Categories.Any(c => c.Id == Id);
        }
        public Product GetById(int Id)
        {
            return _context.Products.Find(Id);
        }
        public async Task<Product> CreateProduct([FromBody] ProductDto dto)
        {
            var product = new Product
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            if (dto.Images != null && dto.Images.Count > 0)
            {
                foreach (var image in dto.Images)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                    var savePath = Path.Combine("wwwroot/images/products", fileName);


                    var directory = Path.GetDirectoryName(savePath);
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    var productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = $"/images/products/{fileName}",
                        IsPrimary = false,
                        CreatedAt = DateTime.Now
                    };

                    _context.ProductImages.Add(productImage);
                    await _context.SaveChangesAsync();
                }

                
            }
           return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = GetById(id);
            if (product == null)
                return null;
            return product;
        } 

        public async Task<Product> DeleteProduct(int id)
        {

            var product = GetById(id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public List<Product>? GetAllProducts()
        {
            var products = _context.Products.ToList();
          if (products == null)
                return null;
          return products;
        }



        public async Task<bool> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }

            if (!isCategoryExist(dto.CategoryId))
            {
                return false;
            }

            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;

        }
    }
}
