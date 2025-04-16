using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System;
using Microsoft.AspNetCore.Identity;

namespace FlexCart.Controllers
{
    public class CompanyController : Controller
    {
        private readonly OnlineDbContext _context;

        public CompanyController(OnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email != null)
            {
                ViewBag.Email = email;
            }
            return View();  
        }

        public async Task<IActionResult> List()
        {
            var companies = await _context.Companies.ToListAsync();
            return View(companies);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC insert_company @CompanyName, @Mobile, @addr,@Email,@web,@password",
                    new SqlParameter("@CompanyName", company.CompanyName),
                    new SqlParameter("@Mobile", company.Mobile),
                    new SqlParameter("@addr", company.Addr),
                    new SqlParameter("@Email", company.Email),
                    new SqlParameter("@password",company.Password),
                    new SqlParameter("@web", company.Web));
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Company added successfully!";
                return RedirectToAction("login","Company");
            }

            return View(company);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();  // If no company found, return 404
            }
            return View(company);  // Pass the company data to the view
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest("mismatch companyid");  // ID mismatch, return Bad Request
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC update_company @CompanyId, @CompanyName, @Mobile, @addr, @Email,@password,@web",
                        new SqlParameter("@CompanyId", Convert.ToInt32(company.CompanyId)),
                        new SqlParameter("@CompanyName", company.CompanyName),
                        new SqlParameter("@Mobile", company.Mobile),
                        new SqlParameter("@addr", company.Addr),
                        new SqlParameter("@Email", company.Email),
                        new SqlParameter("@password", company.Password),
                        new SqlParameter("@web", company.Web));

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit successful!";
                    return RedirectToAction("CompanyList","admin");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Companies.Any(e => e.CompanyId == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(company);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
           {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();  // If no company found, return 404
            }
            return View(company);  // Pass the company data to the view
        }

        [HttpPost]
      
        public async Task<IActionResult> Confirmed_Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound(); // Prevent deleting a non-existent record
            }

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC delete_company @CompanyId",
                    new SqlParameter("@CompanyId", id)); // Pass int directly, no need to convert

                TempData["SuccessMessage"] = "Company deleted successfully!";
                return RedirectToAction("CompanyList","admin"); // Redirect to company list after deletion
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting company: " + ex.Message;
                return RedirectToAction("Delete", new { id }); // ? Redirect to the delete confirmation page
            }
                                // Stay on the same page if an error occurs
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Companies
                .FromSqlRaw("EXEC company_login @Email, @Password",
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", password))
                .ToListAsync();

            if (user.Count > 0)
            {
                // Set session or authentication cookie here
                HttpContext.Session.SetString("UserEmail", email); // Example of setting a session variable
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("index","Company");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email or password.";
                return RedirectToAction("Login");
            }
        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("login","Company"); // This renders Views/Company/Login.cshtml
        }
        [HttpGet]
        public async Task<IActionResult> EditMyProfile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null) return RedirectToAction("Login");

            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.Email == email);

            if (company == null) return NotFound();

            return View("EditMyProfile", company);
        }

        [HttpPost]
        public async Task<IActionResult> EditMyProfile(Company company)
        {
            if (!ModelState.IsValid)
                return View(company);

            try
            {
                var existingCompany = await _context.Companies.FindAsync(company.CompanyId);
                if (existingCompany == null)
                    return NotFound();

                // Use the existing password if user didn't provide a new one
                var updatedPassword = string.IsNullOrWhiteSpace(company.Password)
                    ? existingCompany.Password
                    : company.Password;

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC update_company @CompanyId, @CompanyName, @Mobile, @addr, @Email, @password, @web",
                    new SqlParameter("@CompanyId", company.CompanyId),
                    new SqlParameter("@CompanyName", company.CompanyName),
                    new SqlParameter("@Mobile", company.Mobile),
                    new SqlParameter("@addr", company.Addr),
                    new SqlParameter("@Email", company.Email),
                    new SqlParameter("@password", updatedPassword),
                    new SqlParameter("@web", company.Web));

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Update failed: " + ex.Message;
                return View(company);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteProfile(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return NotFound();

            await _context.Database.ExecuteSqlRawAsync("EXEC delete_company @CompanyId", new SqlParameter("@CompanyId", id));
            HttpContext.Session.Clear(); // log them out

            TempData["SuccessMessage"] = "Your account was deleted.";
            return RedirectToAction("Login");
        }


    }



}
