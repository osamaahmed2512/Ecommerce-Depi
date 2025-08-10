using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IcartService
    {
        Task<IEnumerable<UserCartResponseDto>> GetUserCart(string userId);
        Task<DefaultserviceResponse> Add(string userid, CartDto dto);
        Task<DefaultserviceResponse> update(string id, int productId, UpdateCartDto dto);
        Task<DefaultserviceResponse>? Delete(string Userid, int productid);
    }
}
