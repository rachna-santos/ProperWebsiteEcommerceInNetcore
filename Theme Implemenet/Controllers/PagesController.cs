using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class PagesController : Controller
    {
        private readonly MyDbContext _context;
        public PagesController(MyDbContext context)
        {
                _context = context;
        }
        public IActionResult Index()
        {
            var page = _context.pages.Include(p => p.Module).Include(p=>p.Status).ToList();
            return View(page);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.module = _context.modules.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(PagesModel model)
        {
          
            ViewBag.module = _context.modules.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.pages.Any(p => p.PagesName == model.PagesName);
            if (!isUnique)
            {
                ModelState.AddModelError("PagesName", "PagesName name must be unique.");
                return View(model);
            }
            var page = new Pages();
            page.PagesName = model.PagesName;
            page.CreateDate = DateTime.Now;
            page.Lastmodifield= DateTime.Now;
            page.StatusId = 2;
            page.ModuleId= model.ModuleId;
            _context.pages.Add(page);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index","Pages");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            ViewBag.module = _context.modules.ToList();
            var page=_context.pages.Where(p=>p.PagesId==id).FirstOrDefault();
            return View(page);
        }
        [HttpPost]
        public IActionResult Edit(Pages model)
        {
            ViewBag.status = _context.statuses.ToList();
            ViewBag.module = _context.modules.ToList();
            var isUnique = !_context.pages.Any(p => p.PagesName == model.PagesName);
            if (!isUnique)
            {
                ModelState.AddModelError("PagesName", "PagesName name must be unique.");
                return View(model);
            }
            model.CreateDate = DateTime.Now;
            model.Lastmodifield = DateTime.Now;
            _context.pages.Update(model);
            _context.SaveChanges();
            TempData["edit"] = "update Data successfully";
            return RedirectToAction("Index", "Pages");
        }
        public IActionResult Delete(int id)
        {
            var page = _context.pages.Where(p => p.PagesId == id).FirstOrDefault();
            _context.pages.Remove(page);
            _context.SaveChanges();
            return RedirectToAction("Index", "Pages");
        }

    }
}
