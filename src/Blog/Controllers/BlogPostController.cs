using MarkDonile.Blog.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MarkDonile.Blog.Controllers
{
    public class BlogPostController : Controller
    {
        private IBlogPostRepository _repository;

        public BlogPostController(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List()
        {
            return View(_repository.BlogPosts);
        }
    }
}