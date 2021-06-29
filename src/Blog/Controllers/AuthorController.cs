using System;
using System.Collections.Generic;
using AutoMapper;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarkDonile.Blog.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(
            IAuthorRepository authorRepository,
            IMapper mapper
        )
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            var authors = _authorRepository.GetAll();

            return Ok(authors);
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthor))]
        public ActionResult<Author> GetAuthor(Guid authorId)
        {
            var author = _authorRepository.Get(authorId);

            if (author is null)
            {
                return NotFound(author);
            }
            
            return Ok(author);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Author> PostAuthor(CreateAuthorDto authorToCreate)
        {
            var author = _mapper.Map<Author>(authorToCreate);

            _authorRepository.Add(author);
            _authorRepository.SaveChanges();

            return CreatedAtRoute(
                nameof(GetAuthor),
                new { authorId = author.Id },
                author
            );
        }
    }
}
