using AutoMapper;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MarkDonile.Blog.Controllers
{
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public BlogPostController(
            IBlogPostRepository repository,
            IMapper mapper
        )
        {
            _blogPostRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("blog-posts")]
        public IActionResult GetBlogPosts()
        {
            return Ok(_blogPostRepository.GetAll());
        }

        [HttpGet("blog-posts/{id}", Name = nameof(GetBlogPost))]
        public IActionResult GetBlogPost(int id)
        {
            var blogPost = _blogPostRepository.Get(id);

            if (blogPost is null)
            {
                return NotFound();
            }
            
            return Ok(blogPost);
        }

        [Authorize]
        [HttpPost("blog-posts")]
        public IActionResult CreateBlogPost(CreateBlogPostDto blogPostToCreate)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostToCreate);

            _blogPostRepository.Add(blogPost);
            _blogPostRepository.SaveChanges();

            return CreatedAtRoute(
                nameof(GetBlogPost),
                new { id = blogPost.Id },
                blogPost
            );
        }

        [Authorize]
        [HttpDelete("blog-posts/{blogPostId}")]
        public IActionResult DeleteBlogPost(Guid blogPostId)
        {
            _blogPostRepository.Remove(blogPostId);
            _blogPostRepository.SaveChanges();
            
            return NoContent();
        }

        [Authorize]
        [HttpPut("blog-posts/{blogPostId}")]
        public IActionResult SaveBlogPost(Guid blogPostId, CreateBlogPostDto blogPostDto)
        {
            var blogPost = _blogPostRepository.Get(blogPostId);

            if (blogPost is null)
            {
                return CreateBlogPost(blogPostDto);
            }

            _mapper.Map(blogPostDto, blogPost);

            _blogPostRepository.Update(blogPost);
            _blogPostRepository.SaveChanges();

            return Ok();
        }
    }
}