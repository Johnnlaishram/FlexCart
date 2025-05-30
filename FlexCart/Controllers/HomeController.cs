using FlexCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FlexCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.Email = userEmail; // Send to view
            return View();
        }
       
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        public IActionResult About() {

            return View();
        }
        public IActionResult Home()
        {
            return  RedirectToAction("Index", "Home");
        }
        public IActionResult products()
        {
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
