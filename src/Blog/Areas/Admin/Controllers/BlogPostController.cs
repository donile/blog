using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarkDonile.Blog.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private IBlogPostRepository _repository;
        public BlogPostController(IBlogPostRepository repository)
        {
            _repository = repository;
        }

        public IActionResult List()
        {
            return View(_repository.BlogPosts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BlogPost blogPost)
        {
            if(!ModelState.IsValid)
            {
                return View(nameof(Add), blogPost);
            }

            _repository.Add(blogPost);

            return RedirectToAction(nameof(List));
        }
    }
}