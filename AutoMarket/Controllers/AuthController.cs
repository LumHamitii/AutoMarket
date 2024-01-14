using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMarket.Models; 

namespace AutoMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    // Return additional user details along with the login message
                    return Ok(new { message = "Login successful", userId = user.Id });
                }
                else if (result.RequiresTwoFactor)
                {
                    return BadRequest(new { error = "Two-factor authentication required" });
                }
                else if (result.IsLockedOut)
                {
                    return BadRequest(new { error = "Account locked out" });
                }
            }

            return BadRequest(new { error = "Invalid login attempt" });
        }
    }
}
