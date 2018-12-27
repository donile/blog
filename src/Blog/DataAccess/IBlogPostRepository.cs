using System.Linq;

namespace markdonile.com
{
    public interface IBlogPostRepository
    {
        IQueryable<BlogPost> BlogPosts { get; }
    }
}