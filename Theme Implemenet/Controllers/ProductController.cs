using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Theme_Implemenet.Models;
using Theme_Implemenet.Migrations;
using System.Xml.Linq;

namespace Theme_Implemenet.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public ProductController(MyDbContext context, IWebHostEnvironment hostEnvironment, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
            _roleManager = roleManager;
        }
        [Route("Customer")]
        public IActionResult ViewAll(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CustomerViewModel();
            var cat = (from emp in _context.customers
                       where term == "" || emp.CustName.ToLower().StartsWith(term)
                       select new Customer
                       {
                           CustId = emp.CustId,
                           CustName = emp.CustName,
                           CustEmail = emp.CustEmail,
                           CustNumber = emp.CustNumber,

                       }
            );
            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Customer = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);

        }
        [Route("IndexProduct")]
        public IActionResult Index(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new ProductViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.product.Include(c => c.Category).Include(s => s.SubCategory).Include(c => c.ProductSeason).Include(s => s.Productgender).Include(s => s.Status)
                       where term == "" || emp.productName.ToLower().StartsWith(term)
                       select emp);

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Product = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [HttpGet]
        //[Route("CreateProduct")]
        public IActionResult Create()
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.status = _context.statuses.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model, IFormFile file)
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.status = _context.statuses.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUnique = !_context.product.Any(p => p.productName == model.productName);
            if (!isUnique)
            {
                ModelState.AddModelError("productName", "productName name must be unique");
                return View(model);
            }
            var product = new Product();
            product.productName = model.productName;
            product.productcod = model.productcod;
            product.title = model.title;
            product.description = model.description;
            product.price = model.price;
            product.sizeguideimage = model.sizeguideimage;
            product.categorystyleid = model.categorystyleid;
            product.Keyword = model.Keyword;
            product.categoryId = model.categoryId;
            product.subcategoryId = model.subcategoryId;
            product.productmaterialId = model.productmaterialId;
            product.productseasonId = model.productseasonId;
            product.productgenderId = model.productgenderId;
            product.StatusId = 2;
            product.Lastmodifield = DateTime.Now;
            product.CreateDate = DateTime.Now;
            _context.product.Add(product);
            _context.SaveChanges();

            var uniqueFileName = $"{product.productId}.jpg";
            var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductLogo");
            var filename = "Image/ProductLogo/" + uniqueFileName;
            var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            using (var stream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }
            product.productimage = filename;
            _context.product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.status = _context.statuses.ToList();
            var product = _context.product.Where(p => p.productId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile file)
        {
            ViewBag.cat = _context.categories.ToList();
            ViewBag.style = _context.categoriestyle.ToList();
            ViewBag.sub = _context.subcategories.ToList();
            ViewBag.gender = _context.productgenders.ToList();
            ViewBag.material = _context.productMaterials.ToList();
            ViewBag.season = _context.productSeasons.ToList();
            ViewBag.status = _context.statuses.ToList();
            var product = _context.product.FirstOrDefault(p => p.productId == model.productId);
            if (product != null)
            {
                if (product.productName != model.productName)
                {
                    var isUnique = !_context.product.Any(p => p.productName == model.productName);
                    if (!isUnique)
                    {
                        ModelState.AddModelError("productName", "productName name must be unique");
                        return View(model);
                    }
                }

                product.productName = model.productName;
                product.productcod = model.productcod;
                product.title = model.title;
                product.description = model.description;
                product.sizeguideimage = model.sizeguideimage;
                product.categorystyleid = model.categorystyleid;
                product.price = model.price;
                product.Keyword = model.Keyword;
                product.categoryId = model.categoryId;
                product.subcategoryId = model.subcategoryId;
                product.productmaterialId = model.productmaterialId;
                product.productseasonId = model.productseasonId;
                product.productgenderId = model.productgenderId;
                product.StatusId = model.StatusId;
                product.Lastmodifield = DateTime.Now;
                product.CreateDate = DateTime.Now;

                var uniqueFileName = $"{model.productId}.jpg";

                var imageDirectory = Path.Combine(hostEnvironment.WebRootPath, "Image/ProductLogo");
                var filename = "Image/ProductLogo/" + uniqueFileName;
                var fullImagePath = Path.Combine(imageDirectory, uniqueFileName);
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                using (var stream = new FileStream(fullImagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                product.productimage = filename;
                _context.product.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Product");

            }
            return View(model);
        }
		public IActionResult Createpromotions()
		{
            return View();
		}
        [HttpPost]
        public IActionResult Createpromotions(Promotions model)
        {
            var promotion = !_context.promotions.Any(p => p.ProName == model.ProName);
            if (!promotion)
            {
                ModelState.AddModelError("proName", "proName name must be unique");
                return View(model);
            }
           
                Promotions pro = new Promotions
                {
                    ProName = model.ProName,
                    Value = model.Value,
                    StatusId = 1,
                    StartDate = new DateTime(2024, 6, 7, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    EndDate = new DateTime(2024, 6, 12, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                };
                _context.promotions.Add(pro);
            _context.SaveChanges();
            return RedirectToAction("Createpromotions", "Product");
        }
        public IActionResult promotionMappingIndex(string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new PomotionmappingViewModel();
            //var cat = _context.categories.Include(p => p.Status).ToList();
            var cat = (from emp in _context.promotionmappings.Include(c => c.Product).Include(s => s.Promotions.Status)
                       where term == "" || emp.Product.productName.ToLower().StartsWith(term)
                       select emp);

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.Promotionmapping = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        public IActionResult promotionMapping()
        {
            ViewBag.product = _context.product.ToList();
            ViewBag.promotion = _context.promotions.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult promotionMapping(promotionmodel model)
        {
            ViewBag.product = _context.product.ToList();
            ViewBag.promotion = _context.promotions.ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            };
            var existingPromotion = _context.promotionmappings
                                    .FirstOrDefault(pm => pm.productId == model.productId);

            if (existingPromotion != null)
            {
                // Product already has a promotion, return an error message
                ModelState.AddModelError("productId", "This productId already has a promotion assigned.");
                return View(model);
            }
            Promotionmapping pro = new Promotionmapping
            {
                productId = model.productId,
                proId = model.proId,
            };
            _context.promotionmappings.Add(pro);
            _context.SaveChanges();   
            return RedirectToAction("promotionMappingIndex", "Product");
        }
        public IActionResult promotionMappingEdit(int id)
        {
            ViewBag.product = _context.product.ToList();
            ViewBag.promotion = _context.promotions.ToList();
            var promotion=_context.promotionmappings.Where(x => x.Id == id).FirstOrDefault();   
            return View(promotion);
        }
        [HttpPost]
        public IActionResult promotionMappingEdit(Promotionmapping model)
        {
            ViewBag.product = _context.product.ToList();
            ViewBag.promotion = _context.promotions.ToList();
            var promotion = _context.promotionmappings.FirstOrDefault(x => x.Id == model.Id);
            if (promotion!=null)
            {
                promotion.Id = model.Id;
                promotion.proId= model.proId;   
                promotion.productId=model.productId;               
            }
            _context.promotionmappings.Update(promotion);
            _context.SaveChanges();
            return RedirectToAction("promotionMappingIndex","Product");
        }
        public IActionResult Delete(int id)
        {
            var data = _context.product.Find(id);
            var CurrentImage = Path.Combine(hostEnvironment.WebRootPath, data.productimage);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            };
            _context.product.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        [Route("Inventorylogs")]
        public IActionResult InventoryIndex(string term = "", int currentpage = 1)
        {

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new InventoryModelView();
            var cat = (from emp in _context.inventories.Include(c => c.ProductVeriation.Product).Include(p => p.Customer).Include(c => c.Status).Include(p=>p.ProductVeriation.ProductColor).Include(p=>p.ProductVeriation.ProductSize)
                       where term == "" || emp.Customer.CustEmail.ToLower().StartsWith(term)
                       select emp);
            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);
            catdata.Inventory = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        public IActionResult ViewOrdr(int id)
        {
            var shwodiscountvalue = _context.Orders.Where(p=>p.OrderId==id).Include(p => p.ProductVeriation.Product)
           .Select(p => new
          {
          p.OrderId,
          p.subtotal,
          p.shipping,
          p.totalamount,
          //costprice = p.price-(p.price * 10 / 100),
          p.ProductVeriation.Product.productId,
          p.ProductVeriation.Product.productName,

                  Discount = _context.promotionmappings
                   .Where(pm => pm.productId == p.ProductVeriation.Product.productId)
                   .Select(pm => pm.Promotions.Value)
                     .FirstOrDefault(),
                  //costprice = (p.price - (p.price * (_context.promotionmappings
                  //                               .Where(pm => pm.productId == p.ProductVeriation.Product.productId)
                  //                               .Select(pm => (double?)pm.Promotions.Value)
                  //                               .FirstOrDefault() ?? 0) / 100)),

              }).ToList();
            ViewBag.data = shwodiscountvalue;
           
            var inventory = _context.inventories.Where(p => p.OrderId == id).Include(p => p.Order_Tbl).Include(p => p.Customer).Include(p => p.ProductVeriation.Product).ToList();
            return View(inventory);
        }
        //inventorytable
        public void UpdateInventoryFromOrders()
        {
            var orders = _context.Orders.ToList();
            var qty = 10;

            foreach (var order in orders)
            {
                // Check if the order already exists in the inventory
                bool orderExistsInInventory = _context.inventories.Any(i => i.veriationId == order.veriationId);

                //var orderExistsInInventory = _context.inventories.FirstOrDefault(i =>
                // i.veriationId == order.veriationId &&
                // i.ProductVeriation.productsizeId == order.ProductVeriation.productsizeId &&         // Assuming 'size' is a property of order and inventory
                // i.ProductVeriation.productcolorId == order.ProductVeriation.productcolorId           // Assuming 'color' is a property of order and inventory
                //);


                if (!orderExistsInInventory)
                {
                    // Create a new Inventory entry for each product sold in the order
                    var inventoryItem = new Inventory
                    {
                        CustId = order.CustId,
                        OrderId = order.OrderId,
                        veriationId = order.veriationId,
                        quantity = order.quantity,
                        totalamount = order.totalamount,
                        CreateDate = order.CreateDate,
                        Lastmodifield = order.Lastmodifield,
                        totalqty = 10,
                        Inqty = qty - order.quantity
                    };
                    _context.inventories.Add(inventoryItem);
                }
                //else
                //{
                //    // Update the existing Inventory entry
                //    orderExistsInInventory.Inqty -= order.quantity;
                //    existingInventoryItem.Lastmodifield = DateTime.Now;
                //}
            }
            _context.SaveChanges();
        }

        [Route("Comment")]
        public IActionResult Comment(string term = "", int currentpage = 1)
        {

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new CommentModelView();
            var cat = (from emp in _context.rewies.Include(c => c.ApplicationUser).Include(p => p.Product)
                       where term == "" || emp.Product.productName.ToLower().StartsWith(term)
                       select emp);
            int totalrecord = cat.Count();
            int pagesize = 10;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);

            catdata.rewies = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        public IActionResult PaymentList(int id, string term = "", int currentpage = 1)
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new PaymentModelView();

            var cat = (from emp in _context.payments.Where(p => p.CustId == id).Include(c => c.Customer).Include(s => s.Order).Include(p => p.PaymentType)
                       where term == "" || emp.Customer.CustName.ToLower().StartsWith(term)
                       select emp);

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);
            catdata.payments = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        [Route("OrderList")]
        public IActionResult OderList(string term = "", int currentpage = 1)
        {
        
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var catdata = new OrderModelView();
            var cat = (from emp in _context.Orders.Include(c => c.Customer)
                       where term == "" || emp.Customer.CustEmail.Contains(term)
                       select emp);

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize);
            catdata.Orders = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            return View(catdata);
        }
        //ordershipping
        public IActionResult EditStatus(int id)
        {

            var order = _context.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order != null)
            {
                order.Status = "Shipping";
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            var cart = _context.Orders.Where(p => p.OrderId == id).ToList();
            SendOrderShippingEmail(id);

            return RedirectToAction("OderList", "Product");

        }

        private void SendOrderShippingEmail(int id)
        {
            ViewBag.carts = _context.Orders.Where(p => p.OrderId == id).ToList();

            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;

            var cust = _context.customers.FirstOrDefault(p => p.CustId == userId);
            var Name = cust.CustName;
            var Email = cust.CustEmail;
            string emailBody = $"Dear {Name},\n\n";
            emailBody += "Your Order has been deliverd!\n\n";
            foreach (var item in ViewBag.carts)
            {
                emailBody += $"Quantity: {item.quantity}, SubTotal: {item.subtotal},Shipping:{item.shipping},Total{item.totalamount},Status{item.Status} ,DeliveryDate:{item.CreateDate = DateTime.Now}\n";
            }

            emailBody += "\n\nRegards,\nYour Company";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(Email);
                mail.Subject = "Order Shipping";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Credentials = new NetworkCredential(Email, "wmybqkumcwhrtwwg");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }

            }
        }

        public IActionResult OrderDeliver(int id)
        {
            var order = _context.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order != null)
            {
                order.Status = "Deliverd";
                order.CreateDate = DateTime.Now;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            var carts = _context.Orders.Where(p => p.OrderId == id).ToList();

            SendOrderDeliverdEmail(id);
            return RedirectToAction("OderList", "Product");
        }
        private void SendOrderDeliverdEmail(int id)
        {
            ViewBag.carts = _context.Orders.Where(p => p.OrderId == id).ToList();

            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;

            var cust = _context.customers.FirstOrDefault(p => p.CustId == userId);
            var Name = cust.CustName;
            var Email = cust.CustEmail;
            string emailBody = $"Dear {Name},\n\n";
            emailBody += "Your Order has been deliverd!\n\n";
            foreach (var item in ViewBag.carts)
            {
                emailBody += $"Quantity: {item.quantity}, SubTotal: {item.subtotal},Shipping:{item.shipping},Total{item.totalamount},Status{item.Status} ,DeliveryDate:{item.CreateDate=DateTime.Now}\n";
            }

            emailBody += "\n\nRegards,\nYour Company";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(Email);
                mail.Subject = "Order Deliverd";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Credentials = new NetworkCredential(Email, "wmybqkumcwhrtwwg");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }

            }
        }
        //addinventorytablevalue
        private void CancelledInventoryFromOrders(int id)
        {
            // Fetch the order details using the OrderId
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {
                // Adjust the inventory quantities based on the cancelled order
                var inventoryItem = _context.inventories.FirstOrDefault(i => i.OrderId == order.OrderId);

                if (inventoryItem != null)
                {
                    // Update the inventory quantities by adding the cancelled order quantities back
                    inventoryItem.Inqty += order.quantity;
                    inventoryItem.quantity -= order.quantity;

                    // Update the inventory item in the database
                    _context.inventories.Update(inventoryItem);
                }
                _context.SaveChanges();
            }
        }
        //userrequestcancelledorder
        public IActionResult UserCancelled(int id)
        {
            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            var payment = _context.payments.Where(p => p.OrderId == id).FirstOrDefault();
            var order = _context.Orders.FirstOrDefault(p => p.OrderId == id);
            if (order != null)
            {
                order.Status = "Cancelled";
                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            if (payment != null)
            {
                payment.PaymentStatus = "Refund";
                _context.payments.Update(payment);
                _context.SaveChanges();
            }
            CancelledInventoryFromOrders(id);
            SendUserOrderCancelledEmail(id);
            return RedirectToAction("OderList", "Product");
        }
        private void SendUserOrderCancelledEmail(int id)
        {
            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;
            var cust = _context.customers.FirstOrDefault(p => p.CustId == userId);
            var Name = cust.CustName;
            var Email = cust.CustEmail;

            string emailBody = $"Dear {Name},\n\n";
            emailBody += " Your Order is Cancelled and your cash is refund!\n\n";


            emailBody += "\n\nRegards,\nYour Company";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(Email);
                mail.Subject = "Order Cancelled";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Credentials = new NetworkCredential(Email, "wmybqkumcwhrtwwg");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }

            }
        }
    }
}


