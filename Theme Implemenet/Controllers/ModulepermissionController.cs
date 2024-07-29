using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ModulepermissionController : Controller
    {
        private readonly MyDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ModulepermissionController(MyDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var module = _context.modulepagespermissions.Include(p => p.Module).Include(p => p.ApplicationRole).Include(p => p.Pages).Include(p => p.Status).ToList();
            return View(module);
        }
        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.status = _context.statuses.ToList();
            ViewBag.pages = _context.pages.ToList();
            ViewBag.module = _context.modules.ToList();
            ViewBag.roles = _roleManager.Roles.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ModulepermissModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var modelpermission = new Modulepagespermissions();
            modelpermission.StatusId = 2;
            modelpermission.RoleId = model.RoleId;
            modelpermission.ModuleId = model.ModuleId;
            modelpermission.PagesId=model.PagesId;
            modelpermission.Lastmodifield = DateTime.Now;
            modelpermission.CreateDate = DateTime.Now;
            _context.modulepagespermissions.Add(modelpermission);
            _context.SaveChanges();
            return RedirectToAction("Index","Modulepermission");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewBag.status = _context.statuses.ToList();
            ViewBag.pages = _context.pages.ToList();
            ViewBag.module = _context.modules.ToList();
            ViewBag.roles = _roleManager.Roles.ToList();
            var product = _context.modulepagespermissions.Where(p => p.modulepagespermissionId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Modulepagespermissions model)
        {

            ViewBag.status = _context.statuses.ToList();
            ViewBag.pages = _context.pages.ToList();
            ViewBag.module = _context.modules.ToList();
            ViewBag.roles = _roleManager.Roles.ToList();
            model.CreateDate= DateTime.Now;
            model.Lastmodifield = DateTime.Now;
            _context.modulepagespermissions.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Modulepermission");
        }
        public IActionResult Delete(int id)
        {
            var data = _context.modulepagespermissions.Where(p => p.modulepagespermissionId == id).FirstOrDefault();
            _context.modulepagespermissions.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "Modulepermission");
        }
    }
}
