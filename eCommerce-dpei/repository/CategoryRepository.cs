using AutoMapper;
using eCommerce_dpei.Data;
using eCommerce_dpei.Models;
using eCommerce_dpei.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce_dpei.repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(EcommerceContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Category> Create([FromBody] CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category>? Delete(int id)
        {
           var category = GetById(id);
            if (category == null)
                return null;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public Category? Get(int id)
        {
            var category = GetById(id);
            if (category == null)
                return null;
            return category;
        }

        public  List<Category>? GetAll()
        {
                var categories = _context.Categories.ToList();
                if (categories == null)
                {
                    return null;
                }
                return  categories;
        }

        public Category? GetById(int Id)
        {
            return _context.Categories.Find(Id);
        }

        public async Task<bool> Update(int id, [FromBody] CategoryDto dto)
        {
            var category = GetById(id);
            if (category == null)
            {
                return false;
            }

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.ParentId = dto.ParentId;
            category.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
