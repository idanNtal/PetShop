using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace Service.AccountServices
{

    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public UserManager<IdentityUser> userManager { get { return _userManager; } }

        public async Task<IdentityResult> RegisterUser(RegisterModel model)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public Task<IdentityResult> AddRoleToUser(IdentityUser user, IdentityRole role)
        {
            var result = _userManager.AddToRoleAsync(user, role.Name!);
            return result;
        }

        public Task<IdentityRole> GetRoleByName(string roleName)
        {
            var res = _roleManager.FindByNameAsync(roleName);
            return res;
        }

        public Task<SignInResult> SignInUser(LoginModel model)
        {
            return _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
        }

        public Task SignOutUser()
        {
            return _signInManager.SignOutAsync();
        }

        public Task<List<IdentityUser>> GetAllUsers()
        {
            return _userManager.Users.ToListAsync();
        }

        public Task<IdentityUser> GetUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<IdentityUser> GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IdentityUser> GetUserByName(string name)
        {
            return _userManager.FindByNameAsync(name);
        }

        public Task<IList<string>> GetUserRoles(IdentityUser user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public async Task UpdateUserRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Get the current roles of the user
                var userRoles = await _userManager.GetRolesAsync(user);

                // Remove the user from the roles that are not selected
                var rolesToRemove = userRoles.Except(roles);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                // Add the user to the roles that are selected
                var rolesToAdd = roles.Except(userRoles);
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }
        }

        public Task<List<IdentityRole>> GetAllRoles()
        {
            return _roleManager.Roles.ToListAsync();
        }
    }
}
