using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace markdonile.com
{
    public class DatabaseContext : IdentityDbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}