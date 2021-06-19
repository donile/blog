using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.DataAccess
{
    public class EFAuthorRepository : EFRepository<Author>, IAuthorRepository
    {
        public EFAuthorRepository( DatabaseContext dbContext ) : base( dbContext )
        {

        }
    }
}