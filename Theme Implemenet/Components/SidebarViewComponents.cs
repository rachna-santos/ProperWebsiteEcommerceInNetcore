using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Components
{

    [ViewComponent(Name="Sidebar")]
    public class SidebarViewComponents:ViewComponent
    {
        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public SidebarViewComponents(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, MyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userEmail);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    // Check if the user has the "Admin" role
                    if (roles.Contains("Admin"))
                    {
                        var permissionModel = _context.modulepagespermissions.Where(p => p.RoleId == roles.FirstOrDefault())
                            .Include(p => p.Pages.Module)
                            .Include(p => p.Pages);

                        return View(permissionModel.ToList());
                    }
                }
            }

            // Handle non-admin access here
            return View(); // Ensure you have an "AccessDenied" view to show unauthorized access
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userEmail = User.Identity.Name;
        //        var user = await _userManager.FindByNameAsync(userEmail);
        //        var roleName = await _userManager.GetRolesAsync(user); // Get user roles
        //        //var roles = _roleManager.Roles.Where(p => p.Name == roleName.ToString()).FirstOrDefault(); // Get user roles
        //        var roles = roleName.FirstOrDefault();
        //        var permissionModel = _context.modulepagespermissions.Where(p => p.RoleId == roles)
        //            .Include(p => p.Pages.Module)
        //            .Include(p => p.Pages);

        //        return View(permissionModel.ToList());
        //    }

        //    return View();
        //}
    }
}

