using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
  
    public class CityController : Controller
    {
        private readonly MyDbContext _context;
        public CityController(MyDbContext context)
        {
                _context = context;
        }
        [Route("IndexCity")]
        public IActionResult Index()
        {
        
            var city=_context.cities.Include(p=>p.Country).ToList();
            return View(city);
        }
        [HttpGet]
        [Route("CreateCity")]
        public IActionResult Create()
        {
            ViewBag.country = _context.countries.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUnique = !_context.cities.Any(p => p.cityName == model.cityName);
            if (!isUnique)
            {
                ModelState.AddModelError("cityName", "cityName name must be unique.");
                return View(model);
            }
            var city = new City();
            {
                city.cityName = model.cityName;
                city.countryId = model.countryId;
            };
            _context.cities.Add(city);
            _context.SaveChanges();
            TempData["note"] = "Insert Data successfully";
            return RedirectToAction("Index","City");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            ViewBag.country = _context.countries.ToList();
            var city = _context.cities.Where(p => p.cityId == id).FirstOrDefault();
            return View(city);
        }
        [HttpPost]
        public IActionResult Edit(City model)
        {
            ViewBag.country = _context.countries.ToList();
            var isUnique = !_context.cities.Any(p => p.cityName == model.cityName);
            if (!isUnique)
            {
                ModelState.AddModelError("cityName", "cityName name must be unique.");
                return View(model);
            }
            _context.cities.Update(model);
            _context.SaveChanges();
            TempData["edit"] = "update Data successfully";
            return RedirectToAction("Index", "City");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
          
            var city = _context.cities.Where(p => p.cityId == id).FirstOrDefault();
            _context.cities.Remove(city);
            _context.SaveChanges();
            return RedirectToAction("Index", "City");
        }
    }
}

