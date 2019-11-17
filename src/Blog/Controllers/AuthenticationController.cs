using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarkDonile.Blog.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("api/sign-in")]
        public async Task<IActionResult> SignIn([FromBody] Credentials credentials)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            AppUser user = await _userManager.FindByNameAsync(credentials.Username);

            await _signInManager.SignOutAsync();

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, credentials.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            
            return Ok();
        }

        [HttpPost("api/sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}