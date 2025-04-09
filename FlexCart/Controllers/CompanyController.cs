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

namespace FlexCart.Controllers
{
    public class CompanyController : Controller
    {
        private readonly OnlineDbContext _context;

        public CompanyController(OnlineDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var company = _context.Companies.ToList();
            return View(company);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC insert_company @CompanyName, @Mobile, @addr,@web,@Email",
                    new SqlParameter("@CompanyName", company.CompanyName),
                    new SqlParameter("@Mobile", company.Mobile),
                    new SqlParameter("@addr", company.Addr),
                    new SqlParameter("@Email", company.Email),
                    new SqlParameter("@web", company.Web));
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Company added successfully!";
                return RedirectToAction("Index");
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
                        "EXEC update_company @CompanyId, @CompanyName, @Mobile, @addr, @web, @Email",
                        new SqlParameter("@CompanyId", (company.CompanyId.ToString().Trim())),
                        new SqlParameter("@CompanyName", company.CompanyName),
                        new SqlParameter("@Mobile", company.Mobile),
                        new SqlParameter("@addr", company.Addr),
                        new SqlParameter("@Email", company.Email),
                        new SqlParameter("@web", company.Web));

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Edit successful!";
                    return RedirectToAction("Index");
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
                return RedirectToAction("Index"); // Redirect to company list after deletion
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting company: " + ex.Message;
                return View(company); // Stay on the same page if an error occurs
            }
        }


    }
} 
