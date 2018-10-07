using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace markdonile.com
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}