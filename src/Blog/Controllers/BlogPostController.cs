using MarkDonile.Blog.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace MarkDonile.Blog.Controllers
{
    public class BlogPostController : Controller
    {
        private IBlogPostRepository _repository;

        public BlogPostController( IBlogPostRepository repository )
        {
            _repository = repository;
        }

        public ViewResult List()
        {
            return View();
        }

        [HttpGet("blog-posts/{id}")]
        public IActionResult GetBlogPost(int id)
        {
            var blogPost = _repository.GetBlogPost(id);

            if (blogPost is null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}