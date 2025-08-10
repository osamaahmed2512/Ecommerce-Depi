using Ecommerce.Application.Dto;
using Ecommerce.Application.services;
using Ecommerce.domain.entities;
using Ecommerce.infrastructure.percistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplcationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IAuthservice _authservice;

        public AuthController(ApplcationDbContext context, IConfiguration config, IAuthservice authservice)
        {
            _context = context;
            _config = config;
            _authservice = authservice;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDto dto)
        {
            var result = await _authservice.LogIn(dto);
            if (!result.Success)
            {
                if (result.Errors !=null&&result.Errors.Any())
                {
                    return BadRequest(new { message = result.Message, errors = result.Errors });
                }
                return Unauthorized(new {message = result.Message});
            }
            return Ok(new {message = result.Message , Token = result.Token, RefreshToken = result.RefreshToken});
        }


        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] RegisterDto dto)
        {
            var result = await _authservice.Register(dto);
            if (!result.Success)
            {
                if(result.Errors != null)
                {
                    return BadRequest(new {message= result.Message , errors= result.Errors.FirstOrDefault()});
                }
                return BadRequest(new { message = result.Message });
            }
            return Ok(new {message= result.Message});
        }

        [HttpPost("RefreshToken")]

        public async Task<IActionResult> Refresh([FromBody] TokenModel tokenmodel)
        {
            var result = await _authservice.Refresh(tokenmodel);

            if (!result.Success)
            {

                if (result.Message?.Contains("Internal") == true)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = result.Message });

                return Unauthorized(new { message = result.Message });
            }

            return Ok(new
            {
                accessToken = result.AccessToken,
                refreshToken = result.RefreshToken
            });
        }

        [HttpPost("RevokeToken")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var result = await _authservice.Revoke(User.Identity.Name);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    
    }
}