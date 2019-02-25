using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.EntityFrameworkCore;
using Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkDonile.Blog.DataAccess
{
    public class EFBlogPostRepository : EFRepository<BlogPost>, IBlogPostRepository
    {
        private DatabaseContext _dbContext;

        public EFBlogPostRepository( DatabaseContext dbContext ) : base( dbContext )
        {
            _dbContext = dbContext;
        }

        public Result<BlogPost> GetNewest()
        {
            try
            {
                BlogPost post = 
                    _dbContext.Set<BlogPost>()
                        .OrderByDescending( bp => bp.ReleaseDate )
                        .FirstOrDefault();
                
                return Result.Ok<BlogPost>( post );
            }
            catch( Exception e )
            {
                return Result.Failure<BlogPost>( e.Message );
            }
        }
    }
}