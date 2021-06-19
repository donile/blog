using Microsoft.EntityFrameworkCore;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {            
            modelBuilder.Entity<BlogPost>()
                .Property(bp => bp.Id)
                    .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Author>()
                .Property(a => a.Id)
                    .ValueGeneratedOnAdd();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}