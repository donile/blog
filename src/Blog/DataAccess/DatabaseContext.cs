using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}