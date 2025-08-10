using System.Security.Claims;


namespace Ecommerce.Application.Interfaces
{
    public interface ItokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipleFromExpiredToken(string accessToken);
    }
}
