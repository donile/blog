using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using MarkDonile.Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Results;
using System.Collections.Generic;

namespace MarkDonile.Blog.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private const string ErrorViewName = "Error";
        private IBlogPostRepository _blogPostRepository;
        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public IActionResult List()
        {
            Result<IEnumerable<BlogPost>> result =
                _blogPostRepository.GetAll();

            if ( result.IsError )
            {
                return ErrorView( result.Message );
            }
            
            return View( result.Value );
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add( BlogPost blogPost )
        {
            if ( !ModelState.IsValid )
            {
                return View( nameof( Add ), blogPost );
            }

            Result result = _blogPostRepository.Add( blogPost );

            if ( result.IsError )
            {
                return ErrorView( result.Message );
            }

            return RedirectToAction( nameof( List ) );
        }

        [HttpPost]
        public IActionResult Remove( int? id )
        {
            if ( !id.HasValue )
            {
                return ErrorView( "BlogPost id required to remove." );
            }

            Result result = _blogPostRepository.Remove( id );

            if ( result.IsError )
            {
                return ErrorView( $"Unable to remove post with id {id.Value}" );
            }

            return RedirectToAction( nameof( List ) );
        }

        private IActionResult ErrorView( string message )
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
            };

            return View( ErrorViewName, viewModel );
        }
    }
}