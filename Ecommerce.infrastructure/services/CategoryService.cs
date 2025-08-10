using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using FluentValidation;

namespace Ecommerce.infrastructure.services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitofwork _Unitofwork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _validator;
        public CategoryService(IUnitofwork IUnitofwork , IMapper Mapper, IValidator<CategoryDto> Validator)
        {
            _Unitofwork = IUnitofwork;
            _mapper= Mapper;
            _validator = Validator;
        }

        public async Task Add(CategoryDto Dto)
        {
            var result = await _validator.ValidateAsync(Dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var category = _mapper.Map<Category>(Dto);
                await _Unitofwork.CategoryRepository.Add(category);
                await _Unitofwork.CompleteAsync();
            
        }

        public async Task Delete(int id)
        {
            var Category =await _Unitofwork.CategoryRepository.Get(id);
            if (Category == null) 
                throw new NotFoundException(nameof(Category),id);
            await _Unitofwork.CategoryRepository.Delete(id);
            await _Unitofwork.CompleteAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _Unitofwork.CategoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            var Category =await _Unitofwork.CategoryRepository.Get(id);
            if (Category == null)
                throw new NotFoundException(nameof(Category),id);
            return Category;
        }

        public async Task Update(int id,CategoryDto dto)
        {
            var Category =await  _Unitofwork.CategoryRepository.Get(id);
            if (Category == null)
                throw new NotFoundException(nameof(Category), id);
            
            _mapper.Map(dto, Category);
            Category.UpdatedAt = DateTime.Now;
             _Unitofwork.CategoryRepository.Update(Category);

            await _Unitofwork.CompleteAsync();
        }
    }
}
