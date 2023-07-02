using Microsoft.AspNetCore.Identity;
using WebAppProject.Models;

namespace Service.AccountServices
{
    public interface IAccountService
    {
        public UserManager<IdentityUser> userManager { get; }
        Task<IdentityResult> RegisterUser(RegisterModel model);
        Task<IdentityResult> AddRoleToUser(IdentityUser user, IdentityRole role);
        public Task<IdentityRole> GetRoleByName(string roleName);
        Task<SignInResult> SignInUser(LoginModel model);
        Task SignOutUser();
        Task<List<IdentityUser>> GetAllUsers();
        Task<IdentityUser> GetUserById(string userId);
        Task<IdentityUser> GetUserByEmail(string email);
        Task<IdentityUser> GetUserByName(string name);
        Task<IList<string>> GetUserRoles(IdentityUser user);
        Task UpdateUserRoles(string userId, List<string> roles);
        Task <List<IdentityRole>> GetAllRoles();

    }
}
