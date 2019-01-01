using MarkDonile.Blog.Models;
using System.Linq;

namespace MarkDonile.Blog.DataAccess
{
    public interface IBlogPostRepository
    {
        IQueryable<BlogPost> BlogPosts { get; }
    }
}