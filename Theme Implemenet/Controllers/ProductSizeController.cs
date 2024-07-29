using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductSizeController : Controller
    {
        private readonly  MyDbContext _context;
        public ProductSizeController(MyDbContext context)
        {
                _context=context;
        }
        [Route("IndexProductSize")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new SizeViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.productSizes
                       where term == "" || emp.productsizeName.ToLower().StartsWith(term)
                       select new ProductSize
                         {
                         productsizeId = emp.productsizeId,
                        productsizeName = emp.productsizeName,
               
                    }
                    );

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.ProductSize = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [HttpGet]
        //[Route("CreateProductSize")]
        public IActionResult Create()
        {

            ViewBag.status = _context.statuses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductSizeModel model)
        {

            ViewBag.status = _context.statuses.ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var productsize = new ProductSize();
            productsize.productsizeName = model.productsizeName;    
            productsize.CreateDate = DateTime.Now;
            productsize.Lastmodifield = DateTime.Now;
            productsize.StatusId = 1;
            _context.productSizes.Add(productsize);
            _context.SaveChanges();
            return RedirectToAction("Index","ProductSize");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewBag.status = _context.statuses.ToList();
            var product = _context.productSizes.Where(p => p.productsizeId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductSize model)
        {
            ViewBag.status = _context.statuses.ToList();
     
            model.CreateDate = DateTime.Now;
            model.Lastmodifield = DateTime.Now;
            _context.productSizes.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductSize");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.productSizes.Where(p=>p.productsizeId==id).FirstOrDefault();
            _context.SaveChanges();
            return RedirectToAction("Index", "ProductSize");
        }
    }
}
