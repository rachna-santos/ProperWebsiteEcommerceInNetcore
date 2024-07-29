using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductColorController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductColorController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
                _context= context;
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexProductColor")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new ColorViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.productColors
                       where term == "" || emp.productcolorName.ToLower().StartsWith(term)
                       select new ProductColor
                       {
                            productcolorId= emp.productcolorId,
                            productcolorName = emp.productcolorName,
                            colorcode1= emp.colorcode1,
            
                       }
                        );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.ProductColor = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }

        [HttpGet]
        //[Route("CreateProductColor")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductColorMoodel model,IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var isUnique = !_context.productColors.Any(p => p.productcolorName == model.productcolorName);
            //if (!isUnique)
            //{
            //    ModelState.AddModelError("categoryName", "categoryName name must be unique.");
            //    return View(model);
            //}

            var productcolor = new ProductColor();
            productcolor.productcolorName = model.productcolorName;
            productcolor.colorcode1 = model.colorcode1;
            productcolor.colorcode2 = model.colorcode2;
            productcolor.CreateDate = DateTime.Now;
            productcolor.Lastmodifield = DateTime.Now;
            productcolor.StatusId = 1;
            _context.productColors.Add(productcolor);
            _context.SaveChanges();
            var uniqueFileName = $"{productcolor.productcolorId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductColor");
            var filename = "Image/ProductColor/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

            productcolor.imagepath = filename;
            _context.productColors.Update(productcolor);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "ProductColor");
     
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();

            var product = _context.productColors.Where(p => p.productcolorId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductColor model, IFormFile file,string color,string color1)
        {
            ViewBag.status = _context.statuses.ToList();

            var product = _context.productColors.FirstOrDefault(p => p.productcolorId == model.productcolorId);
            if (product != null)
            {
                if (product.productcolorName != model.productcolorName)
                {
                    var isUnique = !_context.productColors.Any(p => p.productcolorName == model.productcolorName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("categoryName", "categoryName name must be unique.");
                        return View(model);
                    }
                }
                product.productcolorName = model.productcolorName;
                product.colorcode1 = color;
                product.colorcode2 = color1;
                product.CreateDate = DateTime.Now;
                product.Lastmodifield = DateTime.Now;
                product.StatusId = model.StatusId;

                var uniqueFileName = $"{model.productcolorId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductColor");
                var filename = "Image/ProductColor/" + uniqueFileName;
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
                _context.productColors.Update(product);
                _context.SaveChanges();
                TempData["edit"] = "update Data successfully";
                return RedirectToAction("Index", "ProductColor");

            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.productColors.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productColors.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductColor");
        }
    }
}
