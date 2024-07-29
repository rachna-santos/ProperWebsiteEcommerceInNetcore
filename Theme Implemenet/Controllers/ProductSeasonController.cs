using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductSeasonController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductSeasonController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
                _context= context;
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexProductSeason")]
        public IActionResult Index()
        {
            var product = _context.productSeasons.Include(p=>p.Status).ToList();
            return View(product);
        }
        [HttpGet]
        [Route("CreateProductSeason")]
        public IActionResult Create()
        {
            ViewBag.status = _context.statuses.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductSeasonModel model,IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.productSeasons.Any(p => p.productseasonName == model.productseasonName);
            if (!isUnique)
            {
                ModelState.AddModelError("productseasonName", "productseasonName name must be unique.");
                return View(model);
            }
            var productseason = new ProductSeason();
            productseason.productseasonName = model.productseasonName;
            productseason.description = model.description;
            productseason.title = model.title;
            productseason.CreateDate = DateTime.Now;
            productseason.Lastmodifield = DateTime.Now;
            productseason.Keyword = model.Keyword;
            productseason.StatusId = 1;
            productseason.tag = model.tag;
            _context.productSeasons.Add(productseason);
            _context.SaveChanges();
            var uniqueFileName = $"{productseason.productseasonId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Productseason");
            var filename = "Image/Productseason/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);

            }

            productseason.imagepath = filename;
            _context.productSeasons.Update(productseason);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "ProductSeason");
      
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var product = _context.productSeasons.Where(p => p.productseasonId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductSeason model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();


            var newupdate = _context.productSeasons.FirstOrDefault(p => p.productseasonId == model.productseasonId);
            if (newupdate != null)
            {
                if (newupdate.productseasonName != model.productseasonName)
                {
                    var isUnique = !_context.productSeasons.Any(p => p.productseasonName == model.productseasonName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("productseasonName", "productseasonName name must be unique.");
                        return View(model);
                    }
                }

                newupdate.productseasonName = model.productseasonName;
                newupdate.description = model.description;
                newupdate.title = model.title;
                newupdate.CreateDate = DateTime.Now;
                newupdate.Lastmodifield = DateTime.Now;
                newupdate.Keyword = model.Keyword;
                newupdate.StatusId = model.StatusId;
                newupdate.tag = model.tag;
                //_context.productSeasons.Add(newupdate);
                //_context.SaveChanges();
                var uniqueFileName = $"{model.productseasonId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Productseason");
                var filename = "Image/Productseason/" + uniqueFileName;

                var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                using (var stream = new FileStream(fullImagePath, FileMode.Create))
                {
                   await file.CopyToAsync(stream);

                }

                 newupdate.imagepath = filename;
                _context.productSeasons.Update(newupdate);
                _context.SaveChanges();
                TempData["edit"] = "update Data successfully";
                return RedirectToAction("Index", "ProductSeason");

            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.productSeasons.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productSeasons.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductSeason");
        }
     
    }
}
