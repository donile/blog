using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using MarkDonile.Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Results;
using System.Collections.Generic;

namespace MarkDonile.Blog.Controllers
{
    public class HomeController : Controller
    {
        private const string ErrorViewName = "Error";
        private IBlogPostRepository _blogPostRepository;

        public HomeController( IBlogPostRepository blogPostRepository )
        {
            _blogPostRepository = blogPostRepository;
        }
        
        public IActionResult Index()
        {
            Result<BlogPost> result = _blogPostRepository.GetNewest();
            
            if ( result.IsError )
            {
                return ErrorView( result.Message );
            }

            var viewModel = new HomeIndexViewModel
            {
                BlogPost = result.Value,
            };
            
            return View( viewModel );
        }

        public ViewResult Error()
        {
            return View(new Dictionary<string, string>() { ["Message"] = "An unknown error has occurred." });
        }

        private IActionResult ErrorView( string message )
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
            };

            return View ( ErrorViewName, viewModel );
        }
    }
}