using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace markdonile.com
{
    public class UserController : Controller
    {
        private UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}