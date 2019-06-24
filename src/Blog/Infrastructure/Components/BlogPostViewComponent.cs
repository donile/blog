using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Results;
using System.Linq;

namespace MarkDonile.Blog.Infrastructure.ViewComponents
{
    public class BlogPostViewComponent : ViewComponent
    {
        private IBlogPostRepository _repository;

        public BlogPostViewComponent(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke( BlogPost post )
        {
            return View( post );
        }
    }
}