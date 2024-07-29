using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ModuleController : Controller
    {
        private readonly MyDbContext _context;
        public ModuleController(MyDbContext context)
        {
                _context= context;  
        }
        public IActionResult Index()
        {
            var module = _context.modules.Include(p=>p.Status).ToList();
           
            return View(module);
            
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ModuleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.modules.Any(p => p.ModuleName == model.ModuleName);
            if (!isUnique)
            {
                ModelState.AddModelError("ModuleName", "ModuleName name must be unique.");
                return View(model);
            }
            var module = new Module();
            {
                module.ModuleName = model.ModuleName;
                module.CreateDate = DateTime.Now;
                module.Lastmodifield = DateTime.Now;
                module.StatusId=2;
                
            }
            _context.modules.Add(module);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index","Module");
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var module = _context.modules.Where(p => p.ModuleId == id).FirstOrDefault();
            return View(module);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ModuleModel model)
        {
            ViewBag.status = _context.statuses.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.modules.Any(p => p.ModuleName == model.ModuleName);
            if (!isUnique)
            {
                ModelState.AddModelError("ModuleName", "ModuleName name must be unique.");
                return View(model);
            }
            var module = new Module();
            {
                module.ModuleName = model.ModuleName;
                module.CreateDate = DateTime.Now;
                module.Lastmodifield = DateTime.Now;
                module.StatusId = model.StatusId;

            }
            _context.modules.Update(module);
            _context.SaveChanges();
            TempData["edit"] = "Update Data successfully";
            return RedirectToAction("Index", "Module");
        }
        public IActionResult Delete(int id)
        {
            var module = _context.modules.Where(p => p.ModuleId == id).FirstOrDefault();
            _context.modules.Remove(module);
            _context.SaveChanges();
            return RedirectToAction("Index", "Module");
        }
    }
}
