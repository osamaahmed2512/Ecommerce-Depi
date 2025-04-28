using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using System.Linq.Expressions;

namespace eCommerce_dpei.repository
{
    public interface IAddressRepository
    {
        Task<List<CustomerAddress>> GetUserAddresses(int userId);
        Task<CustomerAddress> Create(AddressCreateDto dto, int userId);
        Task<bool> Update(int id, int userId, AddressUpdateDto dto);
        Task<CustomerAddress> Delete(int id, int userId);
        Task<bool> SetDefault(int id, int userId);
        Task<CustomerAddress> Get(Expression<Func<CustomerAddress, bool>> predicate);
    }
}
