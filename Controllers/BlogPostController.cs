using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace markdonile.com
{
    public class BlogPostController : Controller
    {
        private IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository repository)
        {
            _blogPostRepository = repository;
        }

        public ViewResult List()
        {
            return View(_blogPostRepository.BlogPosts());
        }
    }
}