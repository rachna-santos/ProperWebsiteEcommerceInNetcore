using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductBrandController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductBrandController(MyDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context=context;
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexProductBrand")]
        public IActionResult Index()
        {
            var brand = _context.productBrands.Include(p=>p.Status).ToList();
            return View(brand);
        }
        [HttpGet]
        [Route("CreateProductBrand")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrandModel model,IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.productBrands.Any(p => p.productbrandName == model.productbrandName);
            if (!isUnique)
            {
                ModelState.AddModelError("productbrandName", "productbrandName name must be unique.");
                return View(model);
            }
            var brand = new ProductBrand();
            brand.productbrandName = model.productbrandName;
            brand.productbrandphoneNo = model.productbrandphoneNo;
            brand.description = model.description;
            brand.title=model.title;
            brand.tag = model.tag;
            brand.StatusId =2;
            brand.CreateDate = DateTime.Now;
            brand.Lastmodifield =DateTime.Now;
            _context.productBrands.Add(brand);
            _context.SaveChanges();
            var uniqueFileName = $"{brand.productbrandId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductBrand");
            var filename = "Image/ProductBrand/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

            brand.imagepath = filename;
            _context.productBrands.Update(brand);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "ProductBrand");
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var product = _context.productBrands.Where(p => p.productbrandId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductBrand model, IFormFile file)
        {
            var product = _context.productBrands.FirstOrDefault(p => p.productbrandId == model.productbrandId);
            if (product != null)
            {
                if (product.productbrandName != model.productbrandName)
                {
                    var isUnique = !_context.productBrands.Any(p => p.productbrandName == model.productbrandName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("productbrandName", "productbrandName name must be unique.");
                        return View(model);
                    }
                }

                product.productbrandName = model.productbrandName;
                product.productbrandphoneNo = model.productbrandphoneNo;
                product.description = model.description;
                product.title = model.title;
                product.tag = model.tag;
                product.StatusId = model.StatusId;
                product.CreateDate = DateTime.Now;
                product.Lastmodifield = DateTime.Now;
             
                var uniqueFileName = $"{model.productbrandId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductBrand");
                var filename = "Image/ProductBrand/" + uniqueFileName;
                var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                using (var stream = new FileStream(fullImagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }

                product.imagepath = filename;
                _context.productBrands.Update(product);
                _context.SaveChanges();
                TempData["edit"] = "edit Data successfully";
                return RedirectToAction("Index", "ProductBrand");

            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var data = _context.productBrands.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productBrands.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductBrand");
        }
    }
}
