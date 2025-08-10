using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;

namespace Ecommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Add(CategoryDto category);
        Task Update (int id, CategoryDto category);
        Task Delete(int id);
    }
}
