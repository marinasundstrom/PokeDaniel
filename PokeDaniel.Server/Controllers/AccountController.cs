using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PokeDaniel.Server.Data;
using PokeDaniel.Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokeDaniel.Server.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IdentityResult result = await _userManager.CreateAsync(new ApplicationUser()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.Email,
                Email = vm.Email
            }, vm.Password);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok();
        }
    }
}
