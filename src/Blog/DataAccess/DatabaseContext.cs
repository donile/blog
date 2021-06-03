using Microsoft.EntityFrameworkCore;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}