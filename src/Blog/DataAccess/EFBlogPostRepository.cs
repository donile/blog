using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MarkDonile.Blog.DataAccess
{
    public class EFBlogPostRepository : IBlogPostRepository
    {
        private DatabaseContext _dbContext;
        public EFBlogPostRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<BlogPost> BlogPosts => _dbContext.BlogPosts;

        public int Add(BlogPost blogPost)
        {
            _dbContext.BlogPosts.Add(blogPost);
            return _dbContext.SaveChanges();
        }
    }
}