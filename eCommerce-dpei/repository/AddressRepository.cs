using AutoMapper;
using eCommerce_dpei.Data;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce_dpei.repository
{
    public class AddressRepository:IAddressRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerAddress> Create(AddressCreateDto dto, int userId)
        {
            var address = _mapper.Map<CustomerAddress>(dto);
            address.CustomerId = userId;

            if (dto.IsDefault || !await _context.CustomerAddresses.AnyAsync(a => a.CustomerId == userId))
            {
                await UnsetCurrentDefault(userId);
                address.IsDefault = true;
            }

            await _context.CustomerAddresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<CustomerAddress> Delete(int id, int userId)
        {
            var address = await Get(a => a.Id == id && a.CustomerId == userId);
            if (address == null)
                return null;

            _context.CustomerAddresses.Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<CustomerAddress> Get(Expression<Func<CustomerAddress, bool>> predicate)
        {
            return await _context.CustomerAddresses.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<CustomerAddress>> GetUserAddresses(int userId)
        {
            return await _context.CustomerAddresses
                .Where(a => a.CustomerId == userId)
                .OrderByDescending(a => a.IsDefault)
                .ToListAsync();
        }

        public async Task<bool> SetDefault(int id, int userId)
        {
            var address = await Get(a => a.Id == id && a.CustomerId == userId);
            if (address == null)
                return false;

            await UnsetCurrentDefault(userId);
            address.IsDefault = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, int userId, AddressUpdateDto dto)
        {
            var address = await Get(a => a.Id == id && a.CustomerId == userId);
            if (address == null)
                return false;

            _mapper.Map(dto, address);

            if (dto.IsDefault)
            {
                await UnsetCurrentDefault(userId);
                address.IsDefault = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task UnsetCurrentDefault(int userId)
        {
            var currentDefault = await Get(a => a.CustomerId == userId && a.IsDefault);
            if (currentDefault != null)
            {
                currentDefault.IsDefault = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
