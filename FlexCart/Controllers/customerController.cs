
using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace FlexCart.Controllers
{
    public class CustomerController : Controller
    {
        private readonly OnlineDbContext _context;

        public CustomerController(OnlineDbContext context)
        {
            _context = context;
        }

    

        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email != null)
            {
                ViewBag.Email = email;
            }
            var customers = _context.Customers.ToList();
            return View(customers);
        }


        public IActionResult Create()
        {
            return View();
        }
        public IActionResult delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC insert_customer @name, @address, @email, @mobile,@password",
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@mobile", customer.Mobile),
                    new SqlParameter("@password",customer.Password));
                await _context.SaveChangesAsync();
                TempData["SuccessMessage1"] = "Customer added successfully!";
                return RedirectToAction("login","customer");
            }
            return View(customer);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CusId)
            {
                return BadRequest("incorrect ID");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC update_customer @cus_id,@name,@address,@email,@mobile,@password",
                        new SqlParameter("@cus_id",customer.CusId),
                        new SqlParameter("@name", customer.Name),
                        new SqlParameter("@address", customer.Email),
                        new SqlParameter("@email", customer.Address),
                        new SqlParameter("@mobile", customer.Mobile),
                        new SqlParameter("@password", customer.Password));
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Edit sucessfully";
                    return RedirectToAction("CustomerList","admin");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if  (customer== null)
            {
                return  NotFound();//if no company found ,return 404
            }
          return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult>Confirmed_Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            try
            { 
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC delete_customer @cus_id",
                new SqlParameter("@cus_id", id));//pass int directly,no need to convert
            TempData["sucessmessage"] = "company added sucessfully";
            return RedirectToAction("CustomerList","admin");
            }
            catch (Exception ex)
            {
                TempData["error message"] = "error message" + ex.Message;
                    return RedirectToAction("delete", new { id });//redirect to the confirmation page 
            }//stay on the same page if an error occurs
        }
        // GET: /Customer/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // This renders Views/Customer/Login.cshtml
        }

        // POST: /Customer/Login
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
                TempData["successfulMessage"] = "Login successful!";
                return RedirectToAction("index", "Customer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }

        // Logout Action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login", "Customer");
        }

    }

}
