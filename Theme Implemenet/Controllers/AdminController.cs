using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class AdminController : Controller
    {
        public readonly RoleManager<ApplicationRole> _roleManager;
        public readonly UserManager<ApplicationUser> _userManager;

        public readonly MyDbContext _context;
        public AdminController(RoleManager<ApplicationRole> roleManager,MyDbContext context,UserManager<ApplicationUser> userManager)
        {
             _roleManager = roleManager;
            _userManager = userManager;

            _context = context;
        }
        [Route("IndexRole")]
        public async Task<IActionResult> Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CreateRoleViewModel();
            var Role = (from emp in _roleManager.Roles.Where(role => _userManager.Users
             .Any(user => _context.UserRoles
             .Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id && user.CompanyId == role.Company.CompanyId)))
                        where term == "" || emp.Name.ToLower().StartsWith(term)
                        select emp);
            int totalrecord = Role.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            Role = Role.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.ApplicationRole=Role;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totalrecord;

            return View(catdata);
            //var Roles = _roleManager.Roles
            //.Where(role => _userManager.Users
            // .Any(user => _context.UserRoles
            // .Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id && user.CompanyId == role.Company.CompanyId)))
            // .Include(role => role.Company)
            // .Include(role => role.Status)
            // .ToList();

            //var createRoles = Roles.Select(r => new CreateRole
            //{
            //    RoleId = r.Id,
            //    RoleName = r.Name,
            //    StatusId = r.Status.StatusId,
            //    CompanyId = r.Company.CompanyId
            //}).ToList();

            //return View(createRoles);
        }
        [HttpGet]
        //[Route("CreateRole")]
        public IActionResult Create()
        {
            ViewBag.company = _context.companies.ToList();
            ViewBag.status = _context.statuses.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRole model)
        {
            ViewBag.company = _context.companies.ToList();
            ViewBag.status = _context.statuses.ToList();
            ApplicationRole identityRole = new ApplicationRole
            {
                Name = model.RoleName,
                CompanyId = model.CompanyId,
                StatusId=1,
                CreateDate= DateTime.Now,   
                Lastmodifield= DateTime.Now,        
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var roles = await _roleManager.Roles.Where(x => x.Id == Id).FirstOrDefaultAsync();
            CreateRole li = new CreateRole();
            li.RoleId = roles.Id;
            li.RoleName = roles.Name;
            ViewBag.company = _context.companies.ToList();
            ViewBag.status = _context.statuses.ToList(); 
            return View(li);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateRole li)
        {
            var role = await _roleManager.Roles.Where(x => x.Id == li.RoleId).FirstOrDefaultAsync();
            role.Name = li.RoleName;
            role.CompanyId = li.CompanyId;
            role.StatusId = li.StatusId;
            role.CreateDate= DateTime.Now;
            role.Lastmodifield = DateTime.Now;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _roleManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.errorr = "use id {Id} cannot find";
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string Id)
        {
            var user = await _roleManager.FindByIdAsync(Id);

            if (user == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(user);

            if (result.Succeeded)
            return RedirectToAction("Index", "Admin");

            foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

            return View(user);
        }
    }
}
