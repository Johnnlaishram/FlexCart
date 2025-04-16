using Microsoft.AspNetCore.Mvc;//enable mvc feature(controller&view)
using FlexCart.Models; //importing the model folder
using System.Linq; // enable linq query
using System.Threading.Tasks;//enable async/awaite operation
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;//enable the database operations

namespace FlexCart.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineDbContext _context;

        public ProductController(OnlineDbContext context)
        {
            _context = context;
        }
        
        //get product(list all product)
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }
        //get create /product(show create form) this show the empty form when user visit product/create
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult>Create(Product product)
        {
            if (ModelState.IsValid)
            {
                 await _context.Database.ExecuteSqlRawAsync(
               "EXEC insert_product @prod_name, @manf_date, @barcode, @prod_photo, @prod_type_id",
                new SqlParameter("@prod_name", product.ProdName),
                new SqlParameter("@manf_date", product.ManfDate),
                new SqlParameter("@barcode", product.Barcode),
                new SqlParameter("@prod_photo", product.ProdPhoto),
                new SqlParameter("@prod_type_id", product.ProdTypeId));

                await _context.SaveChangesAsync();
                TempData["successMessage"] = "product added sucessfully";
                return RedirectToAction("Index");
            }
            return View(product);   
        }
    }

}
