using System.Security.Claims;
using PokeDaniel.Server.Data;

namespace PokeDaniel.Server.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(ApplicationUser applicationUser);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
}
