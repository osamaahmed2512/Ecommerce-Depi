using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Services
{
    public interface IProductRepository
    {
        bool isCategoryExist(int Id);
        Product GetById(int Id);
        Task<Product> GetProduct(int id);
        List<Product>? GetAllProducts();
        Task CreateProduct([FromBody] ProductDto dto);
        Task<bool> UpdateProduct(int id, [FromBody] ProductDto dto);
        Task<Product> DeleteProduct(int id);
    }
}
