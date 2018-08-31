using System.Collections.Generic;
using System.Linq;

namespace markdonile.com
{
    public class FakeBlogPostRespository : IBlogPostRepository
    {
        public IQueryable<BlogPost> BlogPosts
        {
            get
            {
                return new List<BlogPost>()
                    {
                        new BlogPost(){Title="First Blog Post", Post="Content of First Post", Author="Mark Donile"}
                    }.AsQueryable();
            }
        }
    }
}