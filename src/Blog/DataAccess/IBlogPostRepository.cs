using MarkDonile.Blog.Models;
using Results;
using System.Linq;

namespace MarkDonile.Blog.DataAccess
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        Result<BlogPost> GetNewest();

        BlogPost GetBlogPost(int id);
    }
}