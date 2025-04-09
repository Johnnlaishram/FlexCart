using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
    }
}
