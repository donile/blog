using System;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarkDonile.Blog.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("{authorId}")]
        public ActionResult<Author> GetAuthor(Guid authorId)
        {
            var author = _authorRepository.Get(authorId);

            if (author is null)
            {
                return NotFound(author);
            }
            
            return Ok(author);
        }
    }
}