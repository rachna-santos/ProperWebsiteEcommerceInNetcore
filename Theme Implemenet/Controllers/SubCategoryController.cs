using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public SubCategoryController(MyDbContext context,IWebHostEnvironment hostEnvironment)
        {
                _context= context;
            this.hostEnvironment= hostEnvironment;
        }
        [Route("IndexSubCategory")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new SubCategoryViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.subcategories.Include(s=>s.Category)
                       where term == "" || emp.subcategoryName.ToLower().StartsWith(term)
                       select emp);
            //         {
            //               subcategoryId = emp.subcategoryId,
            //               subcategoryName = emp.subcategoryName,
            //                 title = emp.title,
            //                 categoryId=emp.categoryId,
               
            //}
            // );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.SubCategory = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }

        [HttpGet]
        //[Route("CreateSubCategory")]
        public IActionResult Create()
        {
         ViewBag.category=_context.categories.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubcategoryModel model,IFormFile file)
        {
            ViewBag.category = _context.categories.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.subcategories.Any(p => p.subcategoryName == model.subcategoryName);
            if (!isUnique)
            {
                ModelState.AddModelError("subcategoryName", "subcategoryName name must be unique.");
                return View(model);
            }

            var subcategory = new SubCategory();
            subcategory.subcategoryName = model.subcategoryName;
            subcategory.description = model.description;
            subcategory.title = model.title;
            subcategory.CreateDate = DateTime.Now;
            subcategory.Lastmodifield = DateTime.Now;
            subcategory.Keyword = model.Keyword;
            subcategory.categoryId = model.categoryId;
            subcategory.StatusId = 1;
            subcategory.tag = model.tag;
            _context.subcategories.Add(subcategory);
            _context.SaveChanges();
            var uniqueFileName = $"{subcategory.subcategoryId}.jpg";

            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Subcategory");
            var filename = "Image/Subcategory/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);

            }

            subcategory.imagepath = filename;
             _context.subcategories.Update(subcategory);
             _context.SaveChanges();
             TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "SubCategory");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.category = _context.categories.ToList();
            ViewBag.status = _context.statuses.ToList();
            var sub = _context.subcategories.Where(p => p.subcategoryId == id).FirstOrDefault();
            return View(sub);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubCategory model, IFormFile file)
        {
            ViewBag.status = _context.statuses.ToList();
            ViewBag.category = _context.categories.ToList();
      
            var newupdate = _context.subcategories.FirstOrDefault(p => p.subcategoryId == model.subcategoryId);
            if (newupdate != null)
            {
                if (newupdate.subcategoryName != model.subcategoryName)
                {
                    var isUnique = !_context.subcategories.Any(p => p.subcategoryName == model.subcategoryName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("subcategoryName", "subcategoryName name must be unique.");
                        return View(model);
                    }
                }

                newupdate.subcategoryName = model.subcategoryName;
                newupdate.description = model.description;
                newupdate.title = model.title;
                newupdate.CreateDate = DateTime.Now;
                newupdate.Lastmodifield = DateTime.Now;
                newupdate.Keyword = model.Keyword;
                newupdate.categoryId = model.categoryId;
                newupdate.StatusId = model.StatusId;
                newupdate.tag = model.tag;
                //_context.subcategories.Add(newupdate);
                //await _context.SaveChangesAsync();
                var uniqueFileName = $"{model.subcategoryId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/Subcategory");
                var filename = "Image/Subcategory/" + uniqueFileName;

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
                _context.Entry(newupdate).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["edit"] = "Insert Data successfully";
                return RedirectToAction("Index", "SubCategory");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = _context.subcategories.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.imagepath);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            _context.subcategories.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "SubCategory");
        }
    }
}
