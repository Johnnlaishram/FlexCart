using FlexCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FlexCart.Controllers
{
    public class loginController : Controller
    {
        private readonly OnlineDbContext _context;

        public loginController(OnlineDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Customers
                .FromSqlRaw("EXEC customer_login @email, @password",
                    new SqlParameter("@email", email),
                    new SqlParameter("@password", password))
                 .ToListAsync();
                   

            if (user.Count > 0)
            {
                HttpContext.Session.SetString("UserEmail", email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Index");
            }
        }

        // Logout method - inside the controller!
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
