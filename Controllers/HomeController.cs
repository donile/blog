using Microsoft.AspNetCore.Mvc;

namespace markdonile.com
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}