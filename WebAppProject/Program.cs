using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.AccountServices;
using WebAppProject.DAL;
using WebAppProject.Models;

var builder = WebApplication.CreateBuilder(args);
    
string connectionString = builder.Configuration["ConnectionStrings:PetShop"]!;
builder.Services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<DataContext>();

builder.Services.AddTransient<IPetShopService, PetShopService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Error/PageNotFound/{0}");

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    IdentityRole Role_Admin = new IdentityRole();
    Role_Admin.Name = "Admin";

    await _roleManager.CreateAsync(Role_Admin);
    IdentityRole Role_User = new IdentityRole();
    Role_User.Name = "User";
    await _roleManager.CreateAsync(Role_User);
    IdentityRole Role_Management = new IdentityRole();
    Role_Management.Name = "Management";
    await _roleManager.CreateAsync(Role_Management);

    IdentityUser user1 = new IdentityUser
    {
        UserName = "admin",
        Email = "Admin@Admin.com",
    };

    await _userManager.CreateAsync(user1, "Admin@123");
    await _userManager.AddToRoleAsync(user1, "Admin");

    IdentityUser user2 = new IdentityUser
    {
        UserName = "Dani",
        Email = "Dani12@UserBox.com",

    };

    await _userManager.CreateAsync(user2, "Dani@123");
    await _userManager.AddToRoleAsync(user2, "User");

    IdentityUser user3 = new IdentityUser
    {
        UserName = "Idan",
        Email = "Idan@Admin.com",
        Id = "4da939ba-1b80-4f2c-b6a1-9aa5809cee3d",

    };

    await _userManager.CreateAsync(user3, "Idan@123");
    await _userManager.AddToRolesAsync(user3, new List<string> { "Management" , "Admin", "User"});
}

app.UseStaticFiles();

app.UseAuthentication(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("def", "{controller=Home}/{action=Welcome}");
app.MapDefaultControllerRoute();

app.Run();
