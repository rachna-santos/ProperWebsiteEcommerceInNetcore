using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Net;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Theme_Implemenet.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Theme_Implemenet.Controllers
{
    public class ProductVeriationController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductVeriationController(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(int productId)
        {
            Modelveriation veriationmodel = new Modelveriation();
            var product = _context.product.Where(p => p.productId == productId).FirstOrDefault();

            ViewBag.size = _context.productSizes.ToList();
            ViewBag.color = _context.productColors.ToList();
            ViewBag.status = _context.statuses.ToList();
            ViewBag.Id = product;

            var variations = _context.productVeriations
                    .Include(p => p.ProductSize)
                    .Include(c => c.ProductColor)
                    .Include(s => s.Status)
                    .Where(v => v.productId == productId)
                    .ToList();

            veriationmodel.productVeriations = variations;
            veriationmodel.product1 = product;

            return View(veriationmodel);
        }

        [HttpGet]
        //[Route("CreateProductVeriation")]
        public IActionResult Create()
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.size = _context.productSizes.ToList();
            ViewBag.product = _context.product.ToList();
            ViewBag.color = _context.productColors.ToList();
            ViewBag.status = _context.statuses.ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(veriationModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var variation = new ProductVeriation();
            variation.veriationName = model.veriationName;
            variation.costprice = model.costprice;
            variation.Quantity = model.Quantity;
            variation.RetailerPrice = model.RetailerPrice;
            variation.categoryId = model.categoryId;
            variation.categorystyleid = model.categorystyleid;
            variation.subcategoryId = model.subcategoryId;
            variation.productcolorId = model.productcolorId;
            variation.productgenderId = model.productgenderId;
            variation.productId = model.productId;
            variation.productmaterialId = model.productmaterialId;
            variation.productseasonId = model.productseasonId;
            variation.productsizeId = model.productsizeId;
            variation.StatusId = 2;
            variation.CreateDate = DateTime.Now;
            variation.Lastmodifield = DateTime.Now;
            _context.productVeriations.Add(variation);
            _context.SaveChanges();
            var uniqueFileName = $"{variation.veriationId}.jpg";
            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/productVariation");
            var filename = "Image/productVariation/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }
            variation.image = filename;
            _context.productVeriations.Update(variation);
            _context.SaveChanges();
            TempData["note"] = "data successfully insert";
            return RedirectToAction("Index", "ProductVeriation");
        }
        [HttpGet]
        public IActionResult Edit(int productId)
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.size = _context.productSizes.ToList();
            ViewBag.product = _context.product.ToList();
            ViewBag.color = _context.productColors.ToList();
            ViewBag.status = _context.statuses.ToList();
            var product = _context.productVeriations.Where(p => p.productId == productId).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVeriation model, IFormFile file)
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.size = _context.productSizes.ToList();
            ViewBag.product = _context.product.ToList();
            ViewBag.color = _context.productColors.ToList();
            ViewBag.status = _context.statuses.ToList();
            model.CreateDate = DateTime.Now;
            model.Lastmodifield = DateTime.Now;
            var uniqueFileName = $"{model.veriationId}.jpg";
            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/productVariation");
            var filename = "Image/productVariation/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }
            model.image = filename;
            _context.productVeriations.Update(model);
            _context.SaveChanges();
            TempData["edit"] = "update successfully insert";
            return RedirectToAction("Index", "ProductVeriation");
        }
        public IActionResult Delete(int id)
        {
            var data = _context.productVeriations.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.image);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.productVeriations.Remove(data);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult FormPost(int productId, int[] veriationId, int[] productcolorId, int[] productsizeId, int[] costprice, int[] RetailerPrice, int[] Quantity, int[] status)
        {
            var productmodal = _context.product.Where(p => p.productId == productId).FirstOrDefault();

            if (productmodal == null)
            {
                // Log or print a message to help debug
                Console.WriteLine($"Product with productId {productId} not found.");
            }

            for (int i = 0; i < veriationId.Length; i++)
            {
                if (veriationId[i] == 0)
                {
                    var newVariation = new ProductVeriation
                    {
                        veriationName = "product",
                        productcolorId = productcolorId[i],
                        productsizeId = productsizeId[i],
                        costprice = costprice[i],
                        RetailerPrice = RetailerPrice[i],
                        Quantity = Quantity[i],
                        categoryId = productmodal.categoryId,
                        productId = productmodal.productId,
                        productgenderId = productmodal.productgenderId,
                        productmaterialId = productmodal.productmaterialId,
                        categorystyleid = productmodal.categorystyleid,
                        subcategoryId = productmodal.subcategoryId,
                        productseasonId = productmodal.productseasonId,
                        StatusId = status[i],
                        CreateDate = DateTime.Now,
                        Lastmodifield = DateTime.Now,
                    };
                    _context.productVeriations.Add(newVariation);
                }
                else
                {
                    var existingVariation = _context.productVeriations.FirstOrDefault(v => v.veriationId == veriationId[i]);
                    if (existingVariation != null)
                    {
                        existingVariation.productcolorId = productcolorId[i];
                        existingVariation.productsizeId = productsizeId[i];
                        existingVariation.costprice = costprice[i];
                        existingVariation.RetailerPrice = RetailerPrice[i];
                        existingVariation.Quantity = Quantity[i];
                        existingVariation.StatusId = status[i];
                    }
                }
            }
            _context.SaveChanges();
            //return View("Index", "ProductVeriation");
            return Ok();
        }
        [HttpGet]
        public ActionResult GetImages()
        {
            List<string> imageUrl = GetImageDataFromDatabase();

            return Json(imageUrl);
        }
        private List<string> GetImageDataFromDatabase()
        {
            List<string> imageUrls = _context.productImages.Select(i => i.iamgepath).ToList();

            return imageUrls;
        }
        [HttpPost]
        public IActionResult UpdateImage(int veriationId, string imageUrl)
        {

            // Fetch the product variation from the database based on veriationId
            var productVariation = _context.productVeriations.FirstOrDefault(v => v.veriationId == veriationId);

            if (productVariation != null)
            {
                productVariation.image = imageUrl;
                _context.SaveChanges();
                return Json(new { success = true });
            }

            else
            {
                return Json(new { success = false, message = "Product variation not found." });
            }
        }
        [HttpPost]
        public IActionResult UpdateImages([FromBody] List<ImageModelView> images)
        {
            try
            {
                foreach (var image in images)
                {
                    var productVariation = _context.productVeriations.FirstOrDefault(v => v.veriationId == image.VeriationId);
                    if (productVariation != null)
                    {
                        productVariation.image = image.ImageUrl;
                    }
                    else
                    {
                        return Json(new { success = false, message = $"Product variation with ID {image.VeriationId} not found." });
                    }
                }
                _context.SaveChanges();

                return Json(new { success = true, message = "Images updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating images.", error = ex.Message });
            }
        }

    }
}