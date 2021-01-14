using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Dto;
using Microsoft.AspNetCore.Authorization;
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
            var blogPost = _repository.Get(id);

            if (blogPost is null)
            {
                return NotFound();
            }
            
            return Ok();
        }

        [Authorize]
        [HttpPost("blog-posts")]
        public IActionResult CreateBlogPost(CreateBlogPostDto bpDto)
        {
            if (bpDto is null)
            {
                return BadRequest();
            }
            
            return Created("http://localhost:5000/blog-posts/1", new { FakeProperty="fakeness" });
        }
    }
}