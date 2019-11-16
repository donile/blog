using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;


namespace MarkDonile.Blog.DataAccess
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<AppUser> userManager = serviceProvider.GetService(typeof(UserManager<AppUser>)) as UserManager<AppUser>;

            string userName = configuration["AdminUser:UserName"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];

            AppUser appUser = await userManager.FindByEmailAsync(email);

            if (appUser == null)
            {
                appUser = new AppUser
                {
                    UserName = userName,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, password);

                if (!result.Succeeded){

                    // TODO Log inability to create default admin account
                }
            }
        }
    }
}