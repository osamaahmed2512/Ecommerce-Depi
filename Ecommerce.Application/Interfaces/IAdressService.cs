using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IAdressService
    {
        Task create(string UserId, AddressCreateDto dto);
        Task<DefaultserviceResponse> Update(string UserId, int AddressId, AddressCreateDto dto);
        Task<IEnumerable<Address>> GetALLUserAddress(string userId);
        Task<DefaultserviceResponse> Delete(string userId, int AddressId);
        Task<DefaultserviceResponse> Default(string UserId, int AddressId);
    }
}
