using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using markdonile.com;

namespace markdonile.com
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

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