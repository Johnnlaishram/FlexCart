using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace FlexCart.Controllers
{
    public class AdminController : Controller
    {
        private readonly OnlineDbContext _context;

        public AdminController(OnlineDbContext context)
        {
            _context = context;
        }
        public IActionResult List() 
        { return View(); }  
        public IActionResult Delete ()
        { return View(); }
        public IActionResult Edit()
        { return View(); }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult>Create(Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                           "EXEC insert_admin @email, @password,@user_name",
                           new SqlParameter("@email", admin.Email),
                           new SqlParameter("@password", admin.Password),
                           new SqlParameter("@user_name", admin.UserName));
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Admin added successfully!";
                    return RedirectToAction("Create", "Admin");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the admin: " + ex.Message;
                return RedirectToAction("Create", "Admin");
            }
            return View(admin);
            
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }
        public async Task<IActionResult> Dashboard()
        {
            var company = await _context.Companies.ToListAsync();
            var customer = await _context.Customers.ToListAsync();
            var companyCount = await _context.Companies.CountAsync();
            var customerCount = await _context.Customers.CountAsync();
            var adminCount = await _context.Admins.CountAsync();

            // Pass the data to the view
            ViewBag.Email = HttpContext.Session.GetString("UserEmail");
            ViewBag.AdminCount = adminCount;    
            ViewBag.company = company;
            ViewBag.customer = customer;
            ViewBag.CompanyCount = companyCount;
            ViewBag.CustomerCount = customerCount;

            return View();
        }

        public async Task<IActionResult> CompanyList()
        {
            var companies = await _context.Companies.ToListAsync();
            return View(companies); // Pass the company list to the view
        }

        // Action to display the customer list
        public async Task<IActionResult> CustomerList()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers); // Pass the customer list to the view
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var user = await _context.Admins
                  .FromSqlRaw("EXEC admin_login @Email, @Password",
                      new SqlParameter("@Email", email),
                      new SqlParameter("@Password", password))
                  .ToListAsync();
                if (user.Count > 0) {
                    HttpContext.Session.SetString("UserEmail",email);
                    TempData["SuccessMessage2"] = "Login sucessfully";
                    return RedirectToAction("dashboard","Admin");
                }
                else
                {
                    TempData["error"] = "login failed";
                        return RedirectToAction("login", "Admin");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Product_approval()
        {
            var products = await _context.Products.ToListAsync(); 
            ViewBag.Products = products; // Pass the products to the view
            return View();  

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Admin");
        }
        
    }
}
