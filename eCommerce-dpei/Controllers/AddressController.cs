using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using eCommerce_dpei.Filters;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IAdressService _service;
        public AddressController(IAdressService Service)
        {
             _service = Service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var addresses = await _service.GetALLUserAddress(userId);
                return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressCreateDto dto)
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                 await _service.create(userId,dto);
                return Ok(new { Message = "Address added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressCreateDto dto)
        {

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _service.Update(userId, id, dto);
                if (!result.Success)
                {
                    return StatusCode(result.StatusCode, new {Message= result.Message});
                }
                return Ok(new { Message = "Address updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.Delete(userId,id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, new { Message = result.Message });
            }

            return Ok(new { Message = "Address deleted successfully" });
        }

        [HttpPut("{id}/default")]
        public async Task<IActionResult> SetDefaultAddress(int id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.Default(userId, id);

            if (!result.Success)
            {
                return StatusCode(result.StatusCode,new {Message = result.Message});
            }
            return Ok(new { Message = result.Message });
        }
    }
}
