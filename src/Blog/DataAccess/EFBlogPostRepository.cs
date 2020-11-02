using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class EFBlogPostRepository : EFRepository<BlogPost>, IBlogPostRepository
    {
        public EFBlogPostRepository( DatabaseContext dbContext ) : base( dbContext )
        {

        }
    }
}