using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class UserPermissionController : Controller
    {
        private readonly MyDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserPermissionController(MyDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        [Route("Indexuserpermission")]
        public IActionResult Index()
        {
            var user = _context.userproductpermissions.Include(p => p.ApplicationRole).Include(p => p.Status).ToList();
            return View(user);
        }
        [HttpGet]
        [Route("Createuserpermission")]
        public IActionResult Create()
        {
            ViewBag.user = _userManager.Users.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.roles = _roleManager.Roles.ToList();
            ViewBag.product = _context.product.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Usermodel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new userproductpermission();
            user.RoleId = model.RoleId;
            user.StatusId =2;
            user.CreateDate = model.CreateDate;
            user.Lastmodifield = model.Lastmodifield;
            _context.userproductpermissions.Add(user);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index","UserPermission");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewBag.user = _userManager.Users.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.roles = _roleManager.Roles.ToList();
            ViewBag.product = _context.product.ToList();

            var data = _context.userproductpermissions.Where(p => p.userproductid == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(userproductpermission model)
        {
         
            model.CreateDate = model.CreateDate;
            model.Lastmodifield = model.Lastmodifield;
            _context.userproductpermissions.Update(model);
            _context.SaveChanges();
            TempData["edit"] = "update Data successfully";
            return RedirectToAction("Index", "UserPermission");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _context.userproductpermissions.Where(p => p.userproductid == id).FirstOrDefault();

            _context.userproductpermissions.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "UserPermission");

        }
    }
}

