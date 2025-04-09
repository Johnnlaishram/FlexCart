using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC insert_customer @name, @email, @address, @mobile",
                    new SqlParameter("@name", customer.Name),
                    new SqlParameter("@email", customer.Email),
                    new SqlParameter("@address", customer.Address),
                    new SqlParameter("@mobile", customer.Mobile));
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Customer added successfully!";
                return RedirectToAction("Index");
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
                        "EXEC update_customer @cus_id,@name,@email,@address,@mobile",
                        new SqlParameter("@cus_id",customer.CusId),
                        new SqlParameter("@name", customer.Name),
                        new SqlParameter("@email", customer.Email),
                        new SqlParameter("@address", customer.Address),
                        new SqlParameter("@mobile", customer.Mobile));
                    await _context.SaveChangesAsync();
                    TempData["successMessage"] = "Edit sucessfully";
                    return RedirectToAction("Index");
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

    }

}
