

using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;

namespace Ecommerce.Application.services
{
    public interface IAuthservice
    {
        Task<RegisterResultDto> Register(RegisterDto Dto);
        Task<LogInResultDto> LogIn(LoginDto dto);
        Task<ResponseTokenModelDto> Refresh(TokenModel tokenModel);
        Task<ResponseTokenModelDto> Revoke(string username);
    }
}
