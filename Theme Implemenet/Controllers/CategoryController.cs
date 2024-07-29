using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public CategoryController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }
        [Route("IndexCategory")]
        public IActionResult Index(string term="",int currentpage=1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CategoryViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.categories
                       where term == "" || emp.categoryName.ToLower().StartsWith(term)
                       select new Category
                       {
                           categoryId=emp.categoryId,
                           categoryName = emp.categoryName,
                           title = emp.title,
                           description = emp.description,
                           imagepath = emp.imagepath

                       }
                        ) ; 

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord/(double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Category = cat;
            catdata.currentpage=currentpage;
            catdata.pagesize=pagesize;
            catdata.totalpage=totlapages;
            return View(catdata);
        }
        [HttpGet]
        //[Route("CreateCategory")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.categories.Any(p => p.categoryName == model.categoryName);
            if (!isUnique)
            {
                ModelState.AddModelError("categoryName", "categoryName name must be unique.");
                return View(model);
            }

            var category = new Category();
            category.categoryName = model.categoryName;
            category.description = model.description;
            category.title = model.title;
            category.CreateDate = DateTime.Now;
            category.Lastmodifield = DateTime.Now;
            category.Keyword = model.Keyword;
            category.StatusId = 1;
            category.tag = model.tag;
            _context.categories.Add(category);
            _context.SaveChanges();
            var uniqueFileName = $"{category.categoryId}.jpg";
            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Category");
            var filename = "Image/Category/"+uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);

            }

             category.imagepath = filename;
            _context.categories.Update(category);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            TempData["isSuccess"] = true;

            return RedirectToAction("Index", "Category", new { isSuccess = true });

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            var cat = _context.categories.Where(p=>p.categoryId==id).FirstOrDefault();
            ViewBag.id =id;
            ViewBag.status = _context.statuses.ToList();
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();

            var newproductUpdate = _context.categories.FirstOrDefault(p => p.categoryId == model.categoryId);

            if (newproductUpdate != null)
            {

                if (newproductUpdate.categoryName != model.categoryName)
                {
                    var isUnique = !_context.categories.Any(p => p.categoryName == model.categoryName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("categoryName", "categoryName name must be unique");
                        return View(model);
                    }
                }
                    newproductUpdate.categoryName = model.categoryName;
                    newproductUpdate.description = model.description;
                    newproductUpdate.title = model.title;
                    newproductUpdate.CreateDate = DateTime.Now;
                    newproductUpdate.Lastmodifield = DateTime.Now;
                    newproductUpdate.Keyword = model.Keyword;
                    newproductUpdate.StatusId = model.StatusId;
                    newproductUpdate.tag = model.tag;
                    //_context.Entry(newproductUpdate).State = EntityState.Modified;
                    //_context.SaveChangesAsync();
                   
                    var uniqueFileName = $"{model.categoryId}.jpg";
                    var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Category");
                    var filename = "Image/Category/" + uniqueFileName;
                    var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    using (var stream = new FileStream(fullImagePath, FileMode.Create))
                    {
                       await file.CopyToAsync(stream);

                    }
                      newproductUpdate.imagepath = filename;             
                    _context.Entry(newproductUpdate).State = EntityState.Modified;
                    _context.SaveChanges();
                    TempData["edit"] = "update Data successfully";
                    return RedirectToAction("Index", "Category");

        
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var data = _context.categories.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.categories.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index","Category");
        }

    }
        
}
        
    
    


