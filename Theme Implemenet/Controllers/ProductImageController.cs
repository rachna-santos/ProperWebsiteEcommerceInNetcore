using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Theme_Implemenet.Models;

namespace Theme_Implemenet.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductImageController(MyDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context= context;
            this.hostEnvironment= hostEnvironment;
        }
        //[Route("IndexProductImage")]
        public IActionResult Index()
        {
            var image = _context.productImages.Include(p => p.Product).Include(p => p.Status).ToList();
            return View(image);
        }
        [HttpGet]
        //[Route("CreateProductImage")]
        public IActionResult Create()
        {
            ViewBag.status = _context.statuses.ToList();
            ViewBag.product = _context.product.ToList();
            return View();
        }
        public async Task<IActionResult> Create(ProductImage product, List<IFormFile> file)
        {
            ViewBag.product = _context.product.ToList();

            for (int i = 0; i < file.Count; i++)
            {
                var newProduct = new ProductImage(); // Naya object har bar create karein
                newProduct.productId = product.productId; // Existing product se productId copy karein
                newProduct.StatusId = 2;
                newProduct.CreateDate = DateTime.Now;
                newProduct.Lastmodifield = DateTime.Now;

                var files = file[i];

                if (files.Length > 0)
                {
                    var uniqueFileName = $"{product.productId}_{i + 1}.jpg";
                    var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductImages");
                    var filename = "Image/ProductImages/" + uniqueFileName;
                    var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    using (var stream = new FileStream(fullImagePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }

                    newProduct.iamgepath = filename;
                    newProduct.imagepath_thumb = filename;
                }
                _context.productImages.Add(newProduct);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "ProductImage");
        }

        [HttpGet]
        public IActionResult Edit(int productId1)
        {
              ViewBag.status = _context.statuses.ToList();
            ViewBag.product = _context.product.ToList();
            var images = _context.productImages.Where(img => img.productId == productId1).ToList();

            ViewBag.ProductId = productId1;
            ViewBag.Images = images;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int productId1, List<IFormFile> file)
        {
            //var  productimage=_context.productImages.FirstOrDefault(p=>p.productimageId==model.productimageId);
            var productimage = _context.product.Where(p => p.productId == productId1).FirstOrDefault();

            for (int i = 0; i < file.Count; i++)
            {
                var newproduct = new ProductImage
                {
                    StatusId = productimage.StatusId,
                    productId = productimage.productId,
                    CreateDate = DateTime.Now,
                    Lastmodifield = DateTime.Now,
                };
                //productimage.productId = productId1;
                //productimage.StatusId = model.StatusId;
                //productimage.CreateDate = DateTime.Now;
                //productimage.Lastmodifield = DateTime.Now;
                var files = file[i];

                if (files.Length > 0)
                {

                    var uniqueFileName = $"{newproduct.productId}_{Guid.NewGuid().ToString()}.jpg";
                    var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductImages");
                    var filename = "Image/ProductImages/" + uniqueFileName;
                    var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    using (var stream = new FileStream(fullImagePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);

                    }
                    newproduct.iamgepath = filename;
                    newproduct.imagepath_thumb = filename;
                }
                _context.productImages.Add(newproduct);
            }

            _context.SaveChanges();
            //return Json(new { success = true, message = "Data successfully saved." });
            return Ok();

        }
        public IActionResult Delete(int id)
        {
            
                var data = _context.productImages.Find(id);

                var imagePath = Path.Combine(hostEnvironment.WebRootPath, data.iamgepath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _context.productImages.Remove(data);
                _context.SaveChanges();

            return Ok();
            
            
        }


    }
}
