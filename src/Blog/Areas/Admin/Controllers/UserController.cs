using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MarkDonile.Blog.Admin.ViewModels;
using MarkDonile.Blog.Admin.ViewModels.User;
using System.Linq;
using markdonile.com;

namespace MarkDonile.Blog.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public int PageSize { get; set; } = 4;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageNumber > (_userManager.Users.Count() / PageSize) + 1) { pageNumber = 1; }

            IEnumerable<AppUser> users = _userManager
                .Users
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            PagingInfo pagingInfo = new PagingInfo
            {
                ItemsTotal = _userManager.Users.Count(),
                ItemsPerPage = PageSize,
                PageNumber = pageNumber,
            };

            var viewModel = new UserListViewModel
            {
                AppUsers = users,
                PagingInfo = pagingInfo,
            };

            return View(viewModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(nameof(Index), _userManager.Users);
            }

            IdentityResult result = await _userManager.DeleteAsync(appUser);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(nameof(Index), _userManager.Users);
                }
            }

            return View(nameof(Index), _userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {

            var appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(nameof(Index), _userManager.Users);
            }

            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser appUser = await _userManager.FindByIdAsync(model.Id);

            if (appUser == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            appUser.Email = model.Email;
            appUser.UserName = model.UserName;

            IdentityResult result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // TODO make this an extention method for ModelState
        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }
        }
    }
}