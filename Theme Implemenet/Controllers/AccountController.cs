using MessagePack;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, MyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [Route("listuser")]
        public async Task<IActionResult> Index()
        {
            //term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            //var catdata = new RegisterViewModel();
            //var User = (from emp in _userManager.Users.Where(user => _context.UserRoles
            //         .Any(ur => ur.UserId == user.Id &&
            //                   _roleManager.Roles.Any(role =>
            //                       role.Id == ur.RoleId &&
            //                        role.Company.CompanyId == user.CompanyId))) 
            //           where term == "" || emp.UserName.ToLower().StartsWith(term)
            //           select emp);


            //int totalrecord = User.Count();
            //int pagesize = 5;
            //int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            //User = User.Skip((currentpage - 1) * pagesize).Take(pagesize);

            //catdata.ApplicationUser = User;
            //catdata.currentpage = currentpage;
            //catdata.pagesize = pagesize;
            //catdata.totalpage = totlapages;
            //return View(catdata);
            var users = _userManager.Users
                .Where(user => _context.UserRoles
                   .Any(ur => ur.UserId == user.Id &&
                              _roleManager.Roles.Any(role =>
                                   role.Id == ur.RoleId &&
                                   role.Company.CompanyId == user.CompanyId
                       //role.Name==role.Name
                       )
                    )
                 );
           
            var userModels = new List<ModelView>();
            foreach (var user in users)
            {
                 var userModel = new ModelView
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.PasswordHash,
                        CompanyId = user.CompanyId,
                        StatusId = user.StatusId,
                        Role = await GetUserRoles(user),
                        
                    };
                userModels.Add(userModel);
            }
            return View(userModels);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser item)
        {
            return new List<string>(await _userManager.GetRolesAsync(item));
        }
        [HttpGet]
        //[Route("CreateUser")]
        public IActionResult Register()
        {

            var roles = _roleManager.Roles.Select(r => new CreateRole
            {
                //RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewBag.roles = roles;
            ViewBag.company = _context.companies.ToList();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            var roles = _roleManager.Roles.Select(r => new CreateRole
            {
                //RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewBag.roles = roles;
            ViewBag.company = _context.companies.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    CompanyId = model.CompanyId,
                    StatusId = 2,
                    CreateDate = DateTime.Now,
                    Lastmodifield = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var roles = _roleManager.Roles.Select(r => new CreateRole
            {
                //RoleId = r.Id,
             RoleName = r.Name
            }).ToList();
            ViewBag.roles = roles;
            ViewBag.company = _context.companies.ToList();
            ViewBag.status = _context.statuses.ToList();
            var user= await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            var model = new Register { Id = user.Id, UserName = user.UserName,Email=user.Email, Password = user.PasswordHash};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Register model)
        {
            var roles = _roleManager.Roles.Select(r => new CreateRole
            {
                //RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewBag.roles = roles;
            ViewBag.company = _context.companies.ToList();
            ViewBag.status = _context.statuses.ToList();
            var user = await _userManager.FindByIdAsync(model.Id);

               if (user == null)
                return NotFound();

                 user.Email = model.Email;
                 user.UserName = model.UserName;
                 user.CreateDate= DateTime.Now;
                 user.CompanyId = model.CompanyId;
                user.Lastmodifield= DateTime.Now;
                user.StatusId = model.StatusId;            
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                }
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                var currentRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in currentRoles)
                {
                await _userManager.RemoveFromRoleAsync(user, role);
                }

                 await _userManager.AddToRoleAsync(user, model.RoleName);

                 return RedirectToAction("Index", "Account");

                }
                    foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                    return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
         [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfrim(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index", "Account");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login userModel)
        {
   
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
                {
                    var roles = await _userManager.GetRolesAsync(user); // Get user role

                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    };

                    foreach (var role  in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role)); // Add role claims
                    }

                    var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View();
                }
            }

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
