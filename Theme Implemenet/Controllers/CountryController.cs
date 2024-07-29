using Microsoft.AspNetCore.Mvc;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class CountryController : Controller
    {
        private readonly MyDbContext _context;

        public CountryController(MyDbContext context)
        {
            _context = context;

        }
        [Route("IndexCountry")]
        public IActionResult Index()
        {
            var couuntry=_context.countries.ToList();
            return View(couuntry);
        }
        [HttpGet]
        [Route("CreateCountry")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Country country)
        {
            var isUnique = !_context.countries.Any(p => p.countryName == country.countryName);
            if (!isUnique)
            {
                ModelState.AddModelError("countryName", "countryName name must be unique.");
                return View(country);
            }

            if (ModelState.IsValid)
            {
                _context.countries.Add(country);
                _context.SaveChanges();
                return RedirectToAction("Index","Country");
            }

            return View(country);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var country = _context.countries.Where(p => p.countryId == id).FirstOrDefault();
            return View(country);
        }
        [HttpPost]
        public IActionResult Edit(Country country)
        {
            var isUnique = !_context.countries.Any(p => p.countryName == country.countryName);
            if (!isUnique)
            {
                ModelState.AddModelError("countryName", "countryName name must be unique.");
                return View(country);
            }

            if (ModelState.IsValid)
            {
                _context.countries.Update(country);
                _context.SaveChanges();
                return RedirectToAction("Index", "Country");
            }

            return View(country);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var country = _context.countries.Where(p => p.countryId == id).FirstOrDefault();
                _context.countries.Remove(country);
                _context.SaveChanges();
                return RedirectToAction("Index", "Country");
            }

            return View();
        }
    }
}

