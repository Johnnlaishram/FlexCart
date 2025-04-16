using Microsoft.AspNetCore.Mvc;
using FlexCart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace FlexCart.Controllers
{
    public class ProductController : Controller
    {
        private readonly OnlineDbContext _context;

        public ProductController(OnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Save image to wwwroot/productImages
                if (product.ImageFile != null)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Product_pho");
                    Directory.CreateDirectory(folder);
                    string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    product.ProdPhoto = "/Product_pho/" + fileName;
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC insert_product @prod_name, @barcode, @prod_photo, @prod_type_id",
                    new SqlParameter("@prod_name", product.ProdName),
                    new SqlParameter("@barcode", product.Barcode),
                    new SqlParameter("@prod_photo", product.ProdPhoto),
                    new SqlParameter("@prod_type_id", product.ProdTypeId));

                await _context.SaveChangesAsync();
                TempData["successMessage"] = "Product added successfully!";
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}