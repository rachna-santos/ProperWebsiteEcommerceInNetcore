using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductMaterialController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductMaterialController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
                _context= context;
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexProductMaterial")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new MaterialViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.productMaterials
                       where term == "" || emp.productmaterialname.ToLower().StartsWith(term)
                       select new ProductMaterial
            {
                productmaterialId = emp.productmaterialId,
                productmaterialname = emp.productmaterialname,
                title=emp.title,
                description=emp.description,

            }
             );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.ProductMaterial = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [HttpGet]
        //[Route("CreateProductMaterial")]
        public IActionResult Create()
        {
            ViewBag.status = _context.statuses.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductMaterialModel model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.productMaterials.Any(p => p.productmaterialname == model.productmaterialname);
            if (!isUnique)
            {
                ModelState.AddModelError("productmaterialname", "productmaterialname name must be unique.");
                return View(model);
            }
            var productmaterial = new ProductMaterial();
            productmaterial.productmaterialname = model.productmaterialname;
            productmaterial.description = model.description;
            productmaterial.title = model.title;
            productmaterial.CreateDate = DateTime.Now;
            productmaterial.Lastmodifield = DateTime.Now;
            productmaterial.Keyword = model.Keyword;
            productmaterial.StatusId = 1;
            productmaterial.tag = model.tag;
            _context.productMaterials.Add(productmaterial);
            _context.SaveChanges();
            var uniqueFileName = $"{productmaterial.productmaterialId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductMaterial");
            var filename = "Image/ProductMaterial/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

            productmaterial.imagepath = filename;
            _context.productMaterials.Update(productmaterial);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "ProductMaterial");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var product = _context.productMaterials.Where(p => p.productmaterialId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductMaterial model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();

            var product = _context.productMaterials.FirstOrDefault(p => p.productmaterialId == model.productmaterialId);
            if (product != null)
            {
                if (product.productmaterialname != model.productmaterialname)
                {
                    var isUnique = !_context.productMaterials.Any(p => p.productmaterialname == model.productmaterialname);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("productmaterialname", "productmaterialname name must be unique.");
                        return View(model);
                    }
                }
                product.productmaterialname = model.productmaterialname;
                product.description = model.description;
                product.title = model.title;
                product.CreateDate = DateTime.Now;
                product.Lastmodifield = DateTime.Now;
                product.Keyword = model.Keyword;
                product.StatusId = model.StatusId;
                product.tag = model.tag;
               
                var uniqueFileName = $"{model.productmaterialId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductMaterial");
                var filename = "Image/ProductMaterial/" + uniqueFileName;
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
                _context.productMaterials.Update(product);
                _context.SaveChanges();
                TempData["note"] = "update Data successfully";
                return RedirectToAction("Index", "ProductMaterial");

            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _context.productMaterials.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productMaterials.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductMaterial");
        }
    }
}

