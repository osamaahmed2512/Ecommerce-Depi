using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Application.services;
using Ecommerce.domain.constants;
using Ecommerce.domain.entities;
using Ecommerce.infrastructure.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Ecommerce.infrastructure.services
{
    public class AuthService : IAuthservice
    {
        private readonly UserManager<ApplicationUser> _UserManger;
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly ILogger<AuthService> _logger;
        private readonly ItokenService _service;
        private readonly IValidator<RegisterDto> _validator;
        private readonly IValidator<LoginDto> _Loginvalidator;
        private readonly IUnitofwork _unitofwork;
        public AuthService(UserManager<ApplicationUser> UserManger, RoleManager<IdentityRole> RoleManger,
           ILogger<AuthService> logger, ItokenService service , IValidator<RegisterDto> validator
            ,IValidator<LoginDto> loginvalidator , IUnitofwork Unitofwork )
        {

            _UserManger = UserManger;
            _RoleManger = RoleManger;
            _logger = logger;
            _service = service;
            _validator = validator;
            _Loginvalidator= loginvalidator;
            _unitofwork= Unitofwork;
        }
        public async Task<RegisterResultDto> Register(RegisterDto Dto)
        {
            try
            {
                var result = await _validator.ValidateAsync(Dto);
                if (!result.IsValid)
                {
                    var errors = result.Errors.GroupBy(e => e.PropertyName)
                        .ToDictionary(x =>x.Key, x => x.First().ErrorMessage);
                    return new RegisterResultDto 
                    {
                        Message = "Validation Failed",
                        Success = false,
                        Errors = errors
                    };
                }
                var user =await _UserManger.FindByNameAsync(Dto.Email);

                if (user != null)
                {
                    return new RegisterResultDto
                    {
                        Success = false,
                        Message = "Email Is Already Used"
                    };
                }
                if (!await _RoleManger.RoleExistsAsync(Roles.User))
                {
                    var roleResult = await _RoleManger.CreateAsync(new IdentityRole(Roles.User));
                    if (!roleResult.Succeeded)
                    {
                        return new RegisterResultDto
                        {
                            Success = false,
                            Message = "Falied To Create Role",
                            Errors = roleResult.Errors.ToDictionary(e => e.Code, e => e.Description)
                        };
                    }
                }
                    var addUser = new ApplicationUser
                    {
                        Email = Dto.Email,
                        FirstName = Dto.FirstName,
                        LastName = Dto.LastName,
                        UserName = Dto.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = Dto.Phone,
                        EmailConfirmed = true
                    };
                    var createREsult = await _UserManger.CreateAsync(addUser, Dto.Password);
                    if (!createREsult.Succeeded)
                    {
                        return new RegisterResultDto
                        {
                            Success = false,
                            Message = "User Creation Failed",
                            Errors = createREsult.Errors.ToDictionary(e => e.Code, e => e.Description)
                        };
                    }
                    var RoleResult = await _UserManger.AddToRoleAsync(addUser, Roles.User);
                    if (!RoleResult.Succeeded)
                    {
                        return new RegisterResultDto
                        {
                            Success = false,
                            Message = "Failed To A sign Role ",
                            Errors = RoleResult.Errors.ToDictionary(e => e.Code, e => e.Description)
                        };
                    }

                
                return new RegisterResultDto
                {
                    Success = true,
                    Message = "User registered successfully"
                };
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Registration error");
                return new RegisterResultDto
                {
                    Success = false,
                    Message = "Internal server error"
                };
            }
        }
        public async Task<LogInResultDto> LogIn(LoginDto dto)
        {
            var input = await _Loginvalidator.ValidateAsync(dto);
            if (!input.IsValid)
            {
                return new LogInResultDto
                {
                    Success = false,
                    Message ="Validation Failed",
                    Errors = input.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(e => e.Key, e => e.First().ErrorMessage)
                };
            }
            var existUser =await _UserManger.FindByNameAsync(dto.Email);
            if (existUser ==null || !await _UserManger.CheckPasswordAsync(existUser,dto.Password))
            {
                return new LogInResultDto
                {
                    Success = false,
                    Message = "Invalid User name or passwor"
                };
            }
            List<Claim> authcliams = [
                new (ClaimTypes.NameIdentifier,existUser.Id),
                new (ClaimTypes.Name,existUser.UserName!),
                new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                ];
            var userroles = await _UserManger.GetRolesAsync(existUser);
            foreach (var userrole in userroles)
            {
                authcliams.Add(new Claim(ClaimTypes.Role, userrole));
            }
            var token = _service.GenerateAccessToken(authcliams);

            string RefreshToken = _service.GenerateRefreshToken();

            var TokenInfo =await _unitofwork.TokenRepository.GetWhereFirst(x => x.Username == existUser.UserName);
            if(TokenInfo == null)
            {
                var Ti = new TokenInfo
                {
                    Username = existUser.UserName,
                    RefreshToken = RefreshToken,
                    ExpiredAt = DateTime.UtcNow.AddDays(7),
                };
               await _unitofwork.TokenRepository.Add(Ti);
            }
            else
            {
                TokenInfo.RefreshToken = RefreshToken;
                TokenInfo.ExpiredAt = DateTime.UtcNow.AddDays(7);
            }
            await _unitofwork.CompleteAsync();
            return new LogInResultDto
            {
                Success = true,
                Token = token,
                RefreshToken= RefreshToken,
                Message = "Login successfully"
            };
        }

        public async Task<ResponseTokenModelDto> Refresh(TokenModel tokenModel)
        {
            try
            {
                var principal = _service.GetPrincipleFromExpiredToken(tokenModel.AccessToken);
                var username = principal.Identity.Name;

                var tokenInfo =await _unitofwork.TokenRepository.GetSingle(u => u.Username == username);
                if (tokenInfo == null
                    || tokenInfo.RefreshToken != tokenModel.RefreshToken
                    || tokenInfo.ExpiredAt <= DateTime.UtcNow)
                {
                    return new ResponseTokenModelDto
                        { Message = "Invalid refresh token. Please login again.",
                        Success =false
                    };
                }

                var newAccessToken = _service.GenerateAccessToken(principal.Claims);
                var newRefreshToken = _service.GenerateRefreshToken();

                tokenInfo.RefreshToken = newRefreshToken; 
                await _unitofwork.CompleteAsync();

                return new ResponseTokenModelDto
                {   Success = true,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ResponseTokenModelDto
                {
                    Success = false,
                    Message = "Internal server error during token refresh."
                };
            }
        }
   
        public async Task<ResponseTokenModelDto> Revoke(string username)
        {
            try
            {
                var userToken =await _unitofwork.TokenRepository.GetSingle(x => x.Username == username);
                if (userToken == null)
                {
                    return new ResponseTokenModelDto
                    {
                        Success = false,
                        Message = "user not authorized"
                    };
                }

                await _unitofwork.TokenRepository.Delete(userToken.Id);

                await _unitofwork.CompleteAsync();
                return new ResponseTokenModelDto
                {
                    Success = true,
                    Message = "revoke successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ResponseTokenModelDto
                {
                    Success = false,
                    Message = "Internal server error during token refresh."
                };
            }
        } 
    }
}
