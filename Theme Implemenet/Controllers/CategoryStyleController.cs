using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class CategoryStyleController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public CategoryStyleController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;

            this.hostEnvironment = hostEnvironment;

        }
        [Route("IndexCategoryStyle")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CategoryStyleViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.categoriestyle.Include(c => c.Category).Include(s => s.SubCategory)
                       where term == "" || emp.categorystyleName.ToLower().StartsWith(term)
                       select emp);
                       
            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.CategoryStyle = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }

        [HttpGet]
        //[Route("CreateCategoryStyle")]
        public IActionResult Create()
        {
            ViewBag.category = _context.categories.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.subcategory = _context.subcategories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryStyleModel model,IFormFile file)
        {
            ViewBag.category = _context.categories.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.subcategory = _context.subcategories.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.categoriestyle.Any(p => p.categorystyleName == model.categorystyleName);
            if (!isUnique)
            {
                ModelState.AddModelError("categorystyleName", "categorystyleName name must be unique.");
                return View(model);
            }

            var categorystyle = new CategoryStyle();
            categorystyle.categorystyleName = model.categorystyleName;
            categorystyle.description = model.description;
            categorystyle.title = model.title;
            categorystyle.CreateDate = DateTime.Now;
            categorystyle.Lastmodifield = DateTime.Now;
            categorystyle.Keyword = model.Keyword;
            categorystyle.categoryId = model.categoryId;
            categorystyle.subcategoryId = model.subcategoryId;
            categorystyle.StatusId = 1;
            categorystyle.tag = model.tag;
            _context.categoriestyle.Add(categorystyle);
            _context.SaveChanges();
            var uniqueFileName = $"{categorystyle.categorystyleid}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/CategoryStyle");
            var filename = "Image/CategoryStyle/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);

            }

            categorystyle.imagepath = filename;
            _context.categoriestyle.Update(categorystyle);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "CategoryStyle");

           
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.category = _context.categories.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.subcategory = _context.subcategories.ToList();
            var cat = _context.categoriestyle.Where(p => p.categorystyleid == id).FirstOrDefault();
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryStyle model, IFormFile file)
        {
            ViewBag.category = _context.categories.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.subcategory = _context.subcategories.ToList();


            var newupdate = _context.categoriestyle.FirstOrDefault(p => p.categorystyleid == model.categorystyleid);
            if (newupdate != null)
            {
                if (newupdate.categorystyleName != model.categorystyleName)
                {
                    var isUnique = !_context.categoriestyle.Any(p => p.categorystyleName == model.categorystyleName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("categorystyleName", "categorystyleName name must be unique.");
                        return View(model);
                    }
                }

                newupdate.categorystyleName = model.categorystyleName;
                newupdate.description = model.description;
                newupdate.title = model.title;
                newupdate.CreateDate = DateTime.Now;
                newupdate.Lastmodifield = DateTime.Now;
                newupdate.Keyword = model.Keyword;
                newupdate.categoryId = model.categoryId;
                newupdate.subcategoryId = model.subcategoryId;
                newupdate.StatusId = model.StatusId;
                newupdate.tag = model.tag;
                //_context.categoriestyle.Add(newupdate);
                //_context.SaveChanges();
                var uniqueFileName = $"{newupdate.categorystyleid}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/CategoryStyle");
                var filename = "Image/CategoryStyle/" + uniqueFileName;

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
                _context.categoriestyle.Update(newupdate);
                _context.SaveChanges();
                TempData["edit"] = "update Data successfully";
                return RedirectToAction("Index", "CategoryStyle");


            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = _context.categoriestyle.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.categoriestyle.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "CategoryStyle");
        }
    }
}
