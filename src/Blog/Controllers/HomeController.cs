using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace markdonile.com
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Error()
        {
            return View(new Dictionary<string, string>() { ["Message"] = "An unknown error has occurred." });
        }
    }
}