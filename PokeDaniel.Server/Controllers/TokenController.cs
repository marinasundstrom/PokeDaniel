using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using PokeDaniel.Server.Data;
using PokeDaniel.Server.Models;
using PokeDaniel.Server.Services;

namespace PokeDaniel.Server.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(
                IConfiguration configuration,
                IJwtTokenService tokenService,
                UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(vm.Email);
            bool validCredentials = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!validCredentials)
            {
                return BadRequest("Username or password is incorrect");
            }

            string jwtToken = GenerateToken(user);
            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                token = jwtToken,
                refresh_token = refreshToken
            });
        }

        private string GenerateToken(ApplicationUser applicationUser)
        {
            return _tokenService.BuildToken(applicationUser);
        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(token);
            string email = GetEmailFromClaimsPrincipal(principal);

            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            string savedRefreshToken = user.RefreshToken;

            if (savedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            string newJwtToken = GenerateToken(user);
            string newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                token = newJwtToken,
                refresh_token = newRefreshToken
            });
        }

        private static string GetEmailFromClaimsPrincipal(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
        }

        private string GenerateRefreshToken()
        {
            return _tokenService.GenerateRefreshToken();
        }
    }
}
