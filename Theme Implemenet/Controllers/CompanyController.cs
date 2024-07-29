using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class CompanyController : Controller
    {
        private readonly MyDbContext _context;
        public CompanyController(MyDbContext context)
        {
            _context = context;
        }
        [Route("IndexCompany")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CompanyViewModel();
            //var cat = _context.Include(p => p.Status).ToList();
            var cat = (from emp in _context.companies
                       where term == "" || emp.CompanyName.ToLower().StartsWith(term)
                       select new Company
                       {
                           CompanyId = emp.CompanyId,
                           CompanyName = emp.CompanyName,
                           CompanyEmail=emp.CompanyEmail,
                           ComapnyContactNo = emp.ComapnyContactNo,
                           
                       }
                        );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Companies = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [HttpGet]
        //[Route("CreateCompany")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompanyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          
                var isUnique = !_context.companies.Any(p => p.CompanyName == model.CompanyName);
                if (!isUnique)
                {
                    ModelState.AddModelError("CompanyName", "Company name must be unique.");
                    return View(model);
                }
            var company = new Company();  
            company.CompanyName = model.CompanyName;
            company.ComapnyContactNo = model.ComapnyContactNo;
            company.Address = model.Address;
            company.CompanyEmail = model.ComapnyEmail;
            company.StatusId = 1;  
            company.CreateDate=DateTime.Now;
            company.Lastmodifield=DateTime.Now;
            _context.companies.Add(company);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index", "Company");
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.status = _context.statuses.ToList();
            var data = _context.companies.Where(p => p.CompanyId == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Company model)
        {
            ViewBag.status = _context.statuses.ToList();

            _context.companies.Update(model);
            _context.SaveChanges();
            TempData["edit"] = "Update Data successfully";
            return RedirectToAction("Index", "Company");
        }
    
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _context.companies.Where(p => p.CompanyId == id).FirstOrDefault();

            _context.companies.Remove(data);
                _context.SaveChanges();
                return RedirectToAction("Index", "Company");
       
        }
    }
}
