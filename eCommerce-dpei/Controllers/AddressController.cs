using AutoMapper;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Filters;
using eCommerce_dpei.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce_dpei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(ValidatorFilter))]
    public class AddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressController(IAddressRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var addresses = await _repository.GetUserAddresses(userId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error retrieving addresses: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressCreateDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var address = await _repository.Create(dto, userId);
                return Ok(new { Message = "Address added successfully", Address = address });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error adding address: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressUpdateDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var success = await _repository.Update(id, userId, dto);
                if (!success)
                {
                    return NotFound(new { Message = "Address not found" });
                }
                return Ok(new { Message = "Address updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error updating address: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var address = await _repository.Delete(id, userId);
                if (address == null)
                {
                    return NotFound(new { Message = "Address not found" });
                }
                return Ok(new { Message = "Address deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error deleting address: {ex.Message}" });
            }
        }

        [HttpPut("{id}/default")]
        public async Task<IActionResult> SetDefaultAddress(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var success = await _repository.SetDefault(id, userId);
                if (!success)
                {
                    return NotFound(new { Message = "Address not found" });
                }
                return Ok(new { Message = "Default address updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error updating default address: {ex.Message}" });
            }
        }
    }
}
