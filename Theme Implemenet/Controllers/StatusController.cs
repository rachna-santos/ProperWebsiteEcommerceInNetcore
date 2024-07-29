using Microsoft.AspNetCore.Mvc;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{

    public class StatusController : Controller
    {
        private readonly MyDbContext _context;
        public StatusController(MyDbContext context)
        {
            _context = context;
        }
        [Route("IndexStatus")]
        public IActionResult Index()
        {
            var data = _context.statuses.ToList();
            return View(data);

        }
        [HttpGet]
        [Route("CreateStatus")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Status status)
        {
            
                _context.statuses.Add(status);
                _context.SaveChanges();
                return RedirectToAction("Index", "Status");
          
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var staus = _context.statuses.Where(p => p.StatusId == id).FirstOrDefault();
            return View(staus);
        }
        [HttpPost]
        public IActionResult Edit(Status status)
        {
            if (ModelState.IsValid)
            {

                _context.statuses.Update(status);
                _context.SaveChanges();
                return RedirectToAction("Index", "Status");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var data = _context.statuses.Where(p => p.StatusId == id).FirstOrDefault();
                _context.statuses.Remove(data);
                _context.SaveChanges();
                return RedirectToAction("Index","Status");
            }

            return View();
        }
    }
}