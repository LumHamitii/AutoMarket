using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            var users = _userManager.Users;
            ViewBag.UserManager = _userManager;
            return View(users);
        }

        // Action to change user role
        public async Task<IActionResult> ChangeUserRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

            // Ensure the new role exists
            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                // Create the new role if it doesn't exist
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }

            // Add the user to the new role
            await _userManager.AddToRoleAsync(user, newRole);

            return RedirectToAction(nameof(Index));
        }
    }
  
}
