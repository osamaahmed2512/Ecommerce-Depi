using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.domain.entities;
using Ecommerce.domain.Specification.AddressSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ecommerce.infrastructure.services
{
    public class AddressService : IAdressService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public AddressService(IUnitofwork Unitofwork , IMapper mapper)
        {
            _unitofwork = Unitofwork;
            _mapper = mapper;
        }
        public async Task create(string UserId, AddressCreateDto Dto)
        {
            var address = _mapper.Map<Address>(Dto);
            address.CustomerId = UserId;
            if (Dto.IsDefault)
            {
                var spec = new DefaultAddressSpec(UserId);
                var currentDefault = await _unitofwork.AddressRepository.FindAsync(spec);
                if (currentDefault != null)
                {
                    currentDefault.IsDefault=false;
                    _unitofwork.AddressRepository.Update(currentDefault);
                }
            }
           await _unitofwork.AddressRepository.Add(address);
            await _unitofwork.CompleteAsync();
        }

        public async Task<DefaultserviceResponse> Update(string UserId,int AddressId, AddressCreateDto dto)
        {
            var address =await _unitofwork.AddressRepository.Get(AddressId);
            if (address == null)
            {
                return new DefaultserviceResponse
                {
                    Message = "Address Not Found",
                    Success = false,
                    Type = "Not Found"
                };
            }
            if (address.CustomerId != UserId)
            {
                DefaultserviceResponse.Fail("You Can Only Update Your Own Address", 403);
            }
            if (dto.IsDefault)
            {
                var spec = new DefaultAddressSpec(UserId);
                var currentDefault = await _unitofwork.AddressRepository.FindAsync(spec);
                if (currentDefault != null)
                {
                    currentDefault.IsDefault = false;
                    _unitofwork.AddressRepository.Update(currentDefault);
                }
            }

            _mapper.Map(dto,address );
             _unitofwork.AddressRepository.Update(address);
            await _unitofwork.CompleteAsync();
            return DefaultserviceResponse.Ok("updated successfully");
        }
    
        public async Task<IEnumerable<Address>> GetALLUserAddress(string userId)
        {
            var spec = new GetUserAddressById(userId);
           
           return await _unitofwork.AddressRepository.GetAllAsync(spec);
        }
        public async  Task<DefaultserviceResponse> Delete (string userId, int AddressId)
        {
            var address =await _unitofwork.AddressRepository.Get(AddressId);
            if (address == null)
            {
                return DefaultserviceResponse.Fail($"Address with id : {AddressId} is Not Found", StatusCodes.Status404NotFound);
            }
            if (address.CustomerId != userId)
            {
                return  DefaultserviceResponse.Fail($"you are forbidden to delete other users addresses", StatusCodes.Status403Forbidden);
            }
            await _unitofwork.AddressRepository.Delete(AddressId);
            await _unitofwork.CompleteAsync();
            return DefaultserviceResponse.Ok("Deleted Successfully");
        }
    
        public async Task<DefaultserviceResponse> Default(string UserId, int AddressId)
        {
            var address = await _unitofwork.AddressRepository.Get(AddressId);
            if (address == null)
            {
                return DefaultserviceResponse.Fail($"Adress with ID : {AddressId} Is Not Found",StatusCodes.Status404NotFound);
            }
            if (address.IsDefault)
            {
                return DefaultserviceResponse.Fail($"Address with Id : {AddressId} is Already Default", StatusCodes.Status400BadRequest);
            }
            var spec = new DefaultAddressSpec(UserId);
            var currentDefault = await _unitofwork.AddressRepository.FindAsync(spec);
            if (currentDefault != null )
            {
                currentDefault.IsDefault = false;
                _unitofwork.AddressRepository.Update(currentDefault);
            }
            address.IsDefault= true;
            _unitofwork.AddressRepository.Update(address);
            await _unitofwork.CompleteAsync();
            return DefaultserviceResponse.Ok("Updated successfully");
        }
    
    }
}
