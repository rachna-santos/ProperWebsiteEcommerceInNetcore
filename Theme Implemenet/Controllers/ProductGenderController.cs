using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductGenderController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductGenderController(MyDbContext context,IWebHostEnvironment hostEnvironment)
        {
                _context = context; 
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexProductGender")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new GenderViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.productgenders
                       where term == "" || emp.productgenderName.ToLower().StartsWith(term)
                       select new Productgender
                        {
               productgenderId = emp.productgenderId,
                productgenderName=emp.productgenderName,
                title=emp.title,

            }
             );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Productgender = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [HttpGet]
        [Route("CreateProductGender")]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductGenderModel model,IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.productgenders.Any(p => p.productgenderName == model.productgenderName);
            if (!isUnique)
            {
                ModelState.AddModelError("productgenderName", "productgenderName name must be unique.");
                return View(model);
            }
            var productgebder = new Productgender();
            productgebder.productgenderName = model.productgenderName;
            productgebder.description = model.description;
            productgebder.title = model.title;
            productgebder.CreateDate = DateTime.Now;
            productgebder.Lastmodifield = DateTime.Now;
            productgebder.Keyword = model.Keyword;
            productgebder.StatusId = 1;
            productgebder.tag = model.tag;
            _context.productgenders.Add(productgebder);
            _context.SaveChanges();
            var uniqueFileName = $"{productgebder.productgenderId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Productgender");
            var filename = "Image/Productgender/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

               productgebder.imagepath = filename;
            _context.productgenders.Update(productgebder);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "ProductGender");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var poduct=_context.productgenders.Where(p=>p.productgenderId==id).FirstOrDefault();
            return View(poduct);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Productgender model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();
            var productgender = _context.productgenders.FirstOrDefault(p => p.productgenderId == model.productgenderId);

            if (productgender != null)
            {
                if (productgender.productgenderId != model.productgenderId)
                {
                    var isUnique = !_context.productgenders.Any(p => p.productgenderName == model.productgenderName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("productgenderName", "productgenderName name must be unique.");
                        return View(model);
                    }
                }

                productgender.productgenderName = model.productgenderName;
                productgender.description = model.description;
                productgender.title = model.title;
                productgender.CreateDate = DateTime.Now;
                productgender.Lastmodifield = DateTime.Now;
                productgender.Keyword = model.Keyword;
                productgender.StatusId = model.StatusId;
                productgender.tag = model.tag;
                var uniqueFileName = $"{model.productgenderId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Productgender");
                var filename = "Image/Productgender/" + uniqueFileName;
                var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                using (var stream = new FileStream(fullImagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }

                productgender.imagepath = filename;
                _context.productgenders.Update(productgender);
                _context.SaveChanges();
                TempData["note"] = "update Data successfully";
                return RedirectToAction("Index", "ProductGender");

            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var data = _context.productgenders.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productgenders.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductGender");
        }
    }
}

