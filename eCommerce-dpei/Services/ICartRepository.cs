using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace eCommerce_dpei.Services
{
    public interface ICartRepository
    {   
        
        Cart? GetById(int Id);
        Cart? Get(int id);
        Task<Cart>? Get(Expression<Func<Cart,bool>> predicate);
        Task<Cart> Create([FromBody] CartDto dto,int userId);
        Task<bool> Update(int id,int userId, [FromBody] CartUpdateDto dto);
        Task<Cart>? Delete(int id,int userId);
    }
}
