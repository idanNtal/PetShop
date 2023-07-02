using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.AccountServices;
using System.Data;
using System.Net.Mail;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public delegate Task<List<string>> GetUserRolesDelegate(IdentityUser user);
    public class AccountController : Controller
    {
        private readonly IPetShopService _service;
        private readonly IAccountService _accountService;

        public AccountController(IPetShopService petShopService, IAccountService accountService)
        {
            _service = petShopService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await Task.Run(() => _accountService.SignInUser(model));
                if (result.Succeeded)
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            _accountService.SignOutUser();
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid && model.IsPasswordValid())
            {
                var result = await _accountService.RegisterUser(model);

                if (result.Succeeded)
                    return RedirectToAction("HomePage", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            ViewBag.User = User.Identity!.Name;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Management")]
        public async Task<IActionResult> UsersList()
        {
            ViewBag.UserManager = _accountService.userManager;
            var Users = await _accountService.GetAllUsers();
            return View(Users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Management")]
        public async Task<IActionResult> UserInfo(string userId)
        {
            var user = await _accountService.GetUserById(userId);
            if (user == null)
                return RedirectToAction("IdNotFound", "Error", new { id = userId });

            ViewBag.Roles = await _accountService.GetAllRoles();
            ViewBag.UserRoles = await _accountService.GetUserRoles(user);

            GenericDoubleModel<IdentityUser, IEnumerable<Comment>> model = new GenericDoubleModel<IdentityUser, IEnumerable<Comment>>
            {
                Model_1 = user,
                Model_2 = await _service.CommentService.GetAllCommentsByUserAsync(userId)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            var user = await _accountService.GetUserById(userId);
            // Check if the user is "Idan" and prevent editing
            if (user!.UserName == "Idan")
            {
                // Store the error message in TempData
                TempData["ErrorMessage"] = "Editing this user is not allowed.";
            }
            else if (User.Identity!.Name == user.UserName)
            {
                TempData["ErrorMessage"] = "You can't edit your own user !";
            }
            else
            {
                await _accountService.UpdateUserRoles(userId, roles);
                var updatedUser = await _accountService.GetUserById(userId);
                ViewBag.UserRoles = await _accountService.GetUserRoles(updatedUser);
            }

            return RedirectToAction(nameof(UserInfo), new { userId = userId });
        }

        // ### Remote Attribute (For Register Model) ###
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _accountService.GetUserByEmail(email);
            if (user is null)
                return Json(true);
            else
                return Json($"The provided email - {email} is already in use");
        }

        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await _accountService.GetUserByName(username);
            if (user is null)
                return Json(true);
            else
                return Json($"The provided username - {username} is already in use");
        }


    }
}
