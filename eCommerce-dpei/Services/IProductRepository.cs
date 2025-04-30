using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Services
{
    public interface IProductRepository
    {
        bool isCategoryExist(int Id);
        Product GetById(int Id);
        Task<Product> GetProduct(int id);
        PaginatedProductsDto GetAllProducts(int pagenumber, int pagesize);
        Task<Product> CreateProduct([FromBody] ProductDto dto);
        Task<bool> UpdateProduct(int id, [FromBody] ProductDto dto);
        Task<Product> DeleteProduct(int id);
    }
}
