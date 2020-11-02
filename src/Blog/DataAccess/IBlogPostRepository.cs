using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        BlogPost GetBlogPost(int id);
    }
}