using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.Services
{
    public interface ICategoryRepository
    {
       
        Category? GetById(int Id);
        Category? Get(int id);
        List<Category>? GetAll();
        Task<Category> Create([FromBody] CategoryDto dto);
        Task<bool> Update(int id, [FromBody] CategoryDto dto);
        Task<Category>? Delete(int id);
    }
}
