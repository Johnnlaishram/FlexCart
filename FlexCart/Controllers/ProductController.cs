using Microsoft.AspNetCore.Mvc;//enable mvc feature(controller&view)
using FlexCart.Models; //importing the model folder
using System.Linq; // enable linq query
using System.Threading.Tasks;//enable async/awaite operation
using Microsoft.EntityFrameworkCore;//enable the database operations

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
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdCode,ProdName,ManfDate,Barcode,ProdPhoto,ProdTypeId")] Product product)
        {
            _context.Add(product);  // No validation check
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
