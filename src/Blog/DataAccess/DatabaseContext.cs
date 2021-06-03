using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}