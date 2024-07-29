using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Theme_Implemenet.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Humanizer;
using System.Xml.Schema;
using Microsoft.AspNetCore.Localization;
using System.Net;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using NuGet.Packaging.Signing;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Text;
using Theme_Implemenet.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Policy;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Hosting;
using System.Security.AccessControl;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Configuration;



namespace Theme_Implemenet.Controllers
{
    public class UserController : Controller
    {

        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly string _stripeSecretKey;
        private object JsonRequestBehavior;


        public UserController(MyDbContext context, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _stripeSecretKey = configuration.GetValue<string>("StripeSettings:SecretKey");
            Stripe.StripeConfiguration.ApiKey = _stripeSecretKey;

        }
        public IActionResult Index()
        {

            var proutct = _context.product.ToList();


            string userId = HttpContext.Request.Cookies["UserId"];
            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = cartItems.Sum(item => item.Quantity);
            //int cartItemCount = cartItems.Count;
            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            //discountvalues
            var productsWithDiscounts = _context.product
         .Select(p => new
         {
             p.productId,
             p.productName,
             p.productimage,
             p.price,
             //costprice = p.price-(p.price * 10 / 100),
             Discount = _context.promotionmappings
                      .Where(pm => pm.productId == p.productId)
                      .Select(pm => pm.Promotions.Value)
                        .FirstOrDefault(),
             costprice = (p.price - (p.price * (_context.promotionmappings
                                            .Where(pm => pm.productId == p.productId)
                                            .Select(pm => (double?)pm.Promotions.Value)
                                            .FirstOrDefault() ?? 0) / 100)),
             //enddate=_context.promotionmappings.Where(p=>p.productId==p.productId).Select(p=>p.Promotions.EndDate).FirstOrDefault()

         }).ToList();
            ViewBag.data = productsWithDiscounts;

            return View();
        }
        public IActionResult ProductDetails(int id)
        {
            //discountvaluegetcartvalue
            int? discountValue = _context.promotionmappings
                             .Where(p => p.productId == id)
                             .Select(p => p.Promotions.Value)  // Assuming Discount is the field name
                             .FirstOrDefault();

            // Check if the discount value is not null
            if (discountValue.HasValue)
            {
                // Store the discount value in the session as a string
                HttpContext.Session.SetString("discountValue", discountValue.Value.ToString());
            }
            //getdiscountvaluecheckout
            var promotion = _context.promotionmappings.Where(p => p.productId == id).Select(p => p.Promotions.Value).FirstOrDefault();
            HttpContext.Session.SetString("Promotion", promotion.ToString() ?? "No promotion available");
            string userId = HttpContext.Request.Cookies["UserId"];

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }
            var ratings = new List<Rewies>(); // Define an empty list to store ratings

            //// Check if the user has admin permission
            if (UserHasAdminPermission())
            {
                ratings = _context.rewies
                 .Where(r => r.productId == id)
                 .Include(p => p.ApplicationUser)
                 .ToList();
                ViewBag.Ratings = ratings;
            }
            else
            {
                // User does not have admin permission, only show approved ratings
                ratings = _context.rewies
                 .Include(p => p.ApplicationUser)
                 .Where(r => r.productId == id)
                 .ToList();
            }

            //yahatakcommenthai

            //productid
            ViewBag.id = id;
            //approvelcompan
            //y



            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);
            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            var product = _context.productVeriations.Include(p => p.Product).Where(p => p.productId == id).ToList();
            ViewBag.size = _context.productSizes.ToList();
            ViewBag.color = _context.productColors.ToList();
            ViewBag.ProductName = _context.product.ToList();
            var image = _context.productVeriations.Where(p => p.productId == id).Select(p => p.image).FirstOrDefault();
            ViewBag.image = image;
            var productName = _context.product
                        .Where(p => p.productId == id)
                        .Select(p => p.productName)
                        .FirstOrDefault();
            ViewBag.ProductName = productName;

            var price = _context.product
                        .Where(p => p.productId == id)
                        .Select(p => p.price)
                        .FirstOrDefault();
            var discountprice = price - (price * 10 / 100);
            ViewBag.price = discountprice;

            var costprice = (price - (price * (_context.promotionmappings
                                                        .Where(pm => pm.productId == id)
                                                         .Select(pm => (double?)pm.Promotions.Value)
                                                         .FirstOrDefault() ?? 0) / 100));
            ViewBag.price = costprice;


            var prices = _context.product
                        .Where(p => p.productId == id)
                        .Select(p => p.price)
                        .FirstOrDefault();
            ViewBag.prices = prices;

            int? veriationId = product.FirstOrDefault()?.veriationId;
            ViewBag.veriationId = veriationId;
            return View(product);
        }
        public bool UserHasAdminPermission()
        {
            return User.IsInRole("Admin");
        }
        [HttpPost]
        public IActionResult ProductDetails(int id, int qty, string color, string size, int price, string image)
        {
            //discountvaluegetcart
            string discountValueStr = HttpContext.Session.GetString("discountValue");
            // Convert the discount value back to an integer
            int discountValue = int.Parse(discountValueStr);

            string userId = HttpContext.Request.Cookies["UserId"];

            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            if (userId == null)
            {
                // Redirect to login page if user is not logged in
                return RedirectToAction("Login", "User");
            }
            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            //string UserId = HttpContext.Session.GetString("UserId");
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);
            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            ProductVeriation p = _context.productVeriations.Include(p => p.Product).Where(p => p.veriationId == id).FirstOrDefault();
            ShoppingCarts c = new ShoppingCarts();
            c.veriationId = id;
            c.Size = size;
            c.color = color;
            c.image = image;
            c.Quantity = Convert.ToInt32(qty);
            var discount = price * c.Quantity * discountValue / 100;
            c.Price = price - discount;
            c.bill = c.Price * c.Quantity;
            c.CreateDate = DateTime.Now;
            c.Lastmodifield = DateTime.Now;
            c.UserId = userId;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Add the new item to the cart
            cartItems.Add(c);
            // Update the session with the updated cart items
            HttpContext.Session.SetObject("Cart", cartItems);
            HttpContext.Session.SetString("Price", price.ToString() ?? "No price available");
            return RedirectToAction("Index", "User");

        }
        public IActionResult CheckOut()
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }


            //setdiscountvalue
            var promotion = HttpContext.Session.GetString("Promotion");

            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //total price count
            decimal totalBill = 0;
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");
                // Calculate total bill
                foreach (var item in cartItem)
                {
                    totalBill += item.bill;

                }
            }
            ViewData["TotalBill"] = totalBill;
            ViewData["ShippingAmount"] = 300.00m;
            ViewData["Promotion"] = promotion;
            //ViewBag.TotalBill = totalBill;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);
            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            //Show card data
            var product = _context.productVeriations.Include(p => p.Product).ToList();
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            return View(cartItems);
        }
        [HttpPost]
        public JsonResult Remove(int itemId)
        {
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") == null)
            {
                return Json(new { success = false, message = "Cart is empty" });
            }
            else
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");
                ShoppingCarts c = cartItem.Where(p => p.veriationId == itemId).FirstOrDefault();
                if (c != null)
                {
                    cartItem.Remove(c);
                    decimal totalBill = cartItem.Sum(item => item.bill);
                    HttpContext.Session.SetObject("Cart", cartItem); // Update session
                    return Json(new { success = true, totalBill = totalBill });
                }
                else
                {
                    return Json(new { success = false, message = "Item not found in cart" });
                }
            }
        }
        [HttpGet]
        public IActionResult signup()
        {
            //ViewBag.user = _context.signups.Where(p => p.Id == userid).Select(p => p.Name).FirstOrDefault();

            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            var payment = _context.paymentTypes.ToList();
            ViewBag.Payment = payment;

            decimal totalBill = 0;
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");

                // Calculate total bill
                foreach (var item in cartItem)
                {
                    totalBill += item.bill;
                }
            }
            ViewData["TotalBill"] = totalBill;
            ViewData["ShippingAmount"] = 300.00m;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            return View(cartItems);
        }
        [HttpPost]

        public async Task<IActionResult> signup(RegisterModel model)
        {

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                CompanyId = 1,
                StatusId = 2,
                CreateDate = DateTime.Now,
                Lastmodifield = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Users");
                await _signInManager.SignInAsync(user, isPersistent: false);
                Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMonths(1) // Set cookie expiration date (e.g., 1 month)
                });
                return RedirectToAction("ProcessCheck", "User");
            }

            return View();
        }

   
        //public async Task<IActionResult> signup(Signup model)
        //{
        //    var categories = _context.categories.ToList();
        //    ViewBag.Categories = categories;
        //    var user = new Signup();
        //    user.Id = model.Id;
        //    user.Name = model.Name;
        //    user.Email = model.Email;
        //    user.Password = model.Password;
        //    user.ConfirmPassword = model.ConfirmPassword;
        //    _context.signups.Add(user);
        //    _context.SaveChanges();
        //    return RedirectToAction("processShipping", "User");

        //}
        [HttpGet]
        public IActionResult ProcessCheck()
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }

            var promotion = HttpContext.Session.GetString("Promotion");
            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            var payment = _context.paymentTypes.ToList();
            ViewBag.payment = payment;
            decimal totalBill = 0;
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");
                // Calculate total bill
                foreach (var item in cartItem)
                {
                    totalBill += item.bill;
                    //var Discount = item.Price * item.Quantity * item.Discount / 100;
                    //totalBill += item.Price * item.Quantity - Discount;

                };
            }
            ViewData["TotalBill"] = totalBill;
            ViewData["ShippingAmount"] = 300.00m;
            ViewData["Promotion"] = promotion;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            return View(cartItems);
        }
        [HttpPost]
        public IActionResult ProcessCheck(string name, string email, string password, string number, string address, int PaymentTypeId,string cardNumberInput)
        {

            //Show basket item
            List<ShoppingCarts> carts = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = carts.Sum(item => item.Quantity);
            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            var payment = _context.paymentTypes.ToList();
            ViewBag.Payment = payment;
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            Stripe.StripeConfiguration.ApiKey = _stripeSecretKey;

            Customer cust = new Customer();
            cust.CustName = name;
            cust.CustEmail = email;
            cust.Password = password;
            cust.CustNumber = number;
            cust.Address = address;
            cust.CreateDate = DateTime.Now;
            cust.LastModified = DateTime.Now;
            _context.customers.Add(cust);
            _context.SaveChanges();

            if (PaymentTypeId == 2)
            {

                string token = cardNumberInput switch
                {
                    _ when cardNumberInput.StartsWith("4") => "tok_visa",          // Visa cards start with '4'
                    _ when cardNumberInput.StartsWith("5") => "tok_mastercard",    // Mastercard cards start with '5'
                    _ => "tok_visa"                                                // Default to Visa if not recognized
                };

                var cardTokenOptions = new Stripe.CardCreateOptions
                {
                    Source = token, // Use a test token from Stripe docs
                };
                var cardService = new Stripe.CardService();

                var customerOptions = new Stripe.CustomerCreateOptions
                {
                    Email = email,
                    Name = name,
                    Source = token,
                };
                var customerService = new Stripe.CustomerService();
                Stripe.Customer customer = customerService.Create(customerOptions);

                //create cardtokenid
                var card = cardService.Create(customer.Id, cardTokenOptions);

                var charges = new Stripe.ChargeCreateOptions
                {
                    Amount = (long)((cart.Sum(c => c.bill) + 300) * 100), // amount in cents
                    Currency = "usd",
                    Description = "Order Payment",
                    Source = token,
                };
                var chargesservice = new Stripe.ChargeService();
                Stripe.Charge charge = chargesservice.Create(charges);
            }

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    _context.ShoppingCarts.Add(item);
                    Order od = new Order();
                    od.veriationId = item.veriationId;
                    od.CustId = cust.CustId;
                    od.quantity = item.Quantity;
                    od.subtotal = item.bill;
                    od.shipping = 300;
                    od.totalamount = item.bill + od.shipping;
                    od.Status = "pending";
                    od.CreateDate = DateTime.Now;
                    od.Lastmodifield = DateTime.Now;
                    _context.Orders.Add(od);
                    _context.SaveChanges();
                    Payment pay = new Payment();
                    pay.OrderId = od.OrderId;
                    pay.CustId = cust.CustId;
                    pay.PaymentStatus = "Paid";
                    pay.totalamount = od.totalamount;
                    pay.PaymentTypeId = PaymentTypeId;
                    pay.CreateDate = DateTime.Now;
                    pay.Lastmodifield = DateTime.Now;
                    _context.payments.Add(pay);
                    _context.SaveChanges();
                    UpdateInventoryFromOrders();
                    SendOrderConfirmationEmail(cust, cart);
                }
            }

            //_context.SaveChanges();
            //UpdateInventoryFromOrders();
            //SendOrderConfirmationEmail(cust, cart);
            else
            {
                ViewBag.alert = @"<script language='javascript'>alert('plaese select product');</script>";
            }
            return RedirectToAction("Index", "User");

        }
        private void SendOrderConfirmationEmail(Customer customer, List<ShoppingCarts> cart)
        {
            string emailBody = $"Dear {customer.CustName},\n\n";
            emailBody += "Thank you for your order!\n\n";
            emailBody += "Below are the details of your order:\n\n";

            foreach (var item in cart)
            {
                emailBody += $"Item: {item.Size}, Color: {item.color},Quantity: {item.Quantity}, Total: {item.bill}\n";
            }

            emailBody += "\n\nRegards,\nYour Company";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(customer.CustEmail);
                mail.Subject = "Order Confirmation";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Credentials = new NetworkCredential(customer.CustEmail, "wmybqkumcwhrtwwg");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
        }

        [HttpGet]
        //goodle integration
        //public IActionResult Login(string returnUrl = "/")
        //{

        //    TempData["ReturnUrl"] = returnUrl;

        //    // Redirect the user to Google for authentication
        //    return Challenge(new AuthenticationProperties { RedirectUri = Url.Action(nameof(GoogleResponse)) }, GoogleDefaults.AuthenticationScheme);

        //    //await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
        //    //{
        //    //    RedirectUri = Url.Action("GoogleResponse")
        //    //});
        //    var payment = _context.paymentTypes.ToList();
        //    ViewBag.Payment = payment;

        //    decimal totalBill = 0;
        //    if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
        //    {
        //        List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");

        //        // Calculate total bill
        //        foreach (var item in cartItem)
        //        {
        //            totalBill += item.bill;
        //        }
        //    }

        //    ViewData["TotalBill"] = totalBill;
        //    ViewData["ShippingAmount"] = 300.00m;
        //    //Show basket item
        //    List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

        //    // Calculate the count of cart items
        //    int cartItemCount = cart.Count;

        //    // Store the count in ViewBag
        //    ViewBag.CartItemCount = cartItemCount;
        //    List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
        //    return View(cartItems);
        //}
        public IActionResult Login()
        {
            var payment = _context.paymentTypes.ToList();
            ViewBag.Payment = payment;

            decimal totalBill = 0;
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");

                // Calculate total bill
                foreach (var item in cartItem)
                {
                    totalBill += item.bill;
                }
            }

            ViewData["TotalBill"] = totalBill;
            ViewData["ShippingAmount"] = 300.00m;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            return View(cartItems);
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login userModel)
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

                    foreach (var role in roles)
                   {
                        claims.Add(new Claim(ClaimTypes.Role, role)); // Add role claims
                   }

                    var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));
                //HttpContext.Session.SetString("UserId", user.Id);
                // DateTime expirationTime = DateTime.Now.AddHours(2);
                Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
                {
                   Expires = DateTime.UtcNow.AddMonths(1) // Set cookie expiration date (e.g., 1 month)
                 });

                return RedirectToAction("processShipping", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View();
                }

        }
        //public async Task<IActionResult> Login(string email, string password)
        //{

        //    var user = _context.signups.FirstOrDefault(u => u.Email == email && u.Password == password);

        //    if (user != null)
        //    {
        //        //ViewBag.UserId = user.Id;

        //        //return RedirectToAction("processShipping", "User", new { Id = user.Id });
        //        Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
        //        {
        //            Expires = DateTime.UtcNow.AddMonths(1) // Set cookie expiration date (e.g., 1 month)
        //        });

        //        return RedirectToAction("processShipping", "User");

        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid Email or Password");
        //        return View();
        //    }

        //}
        public async Task<IActionResult> GoogleResponse()
        {
            var returnUrl = TempData["ReturnUrl"] as string ?? "/";

            // Perform the external login/sign up logic
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (authenticateResult.Succeeded)
            {
                HttpContext.User = authenticateResult.Principal;

                // Check if user is authenticated
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    //var token = Guid.NewGuid().ToString();               
                    //ViewBag.Token = token;
                    var userId = HttpContext.Session.GetString("Id");
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // User ID found, perform the necessary action
                        return RedirectToAction("processShipping", "User");
                    }
                }
            }

            else
            {

                return RedirectToAction("Login", "User");
            }
            return RedirectToAction("Login", "User");
        }
        //public async Task<IActionResult> LogOut()
        //{
        //    Response.Cookies.Delete("UserId");

        //    return RedirectToAction("Index", "User");
        //}
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult processShipping()
        {

            var promotion = HttpContext.Session.GetString("Promotion");
            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            string userId = HttpContext.Request.Cookies["UserId"];

            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }


            //if (userId)
            //{
            //    var user = _context.registers.FirstOrDefault(p => p.Id == userId);
            //    var Name = user.Email;
            //    ViewBag.UserName = Name;
            //}

            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            var payment = _context.paymentTypes.ToList();
            ViewBag.payment = payment;
            decimal totalBill = 0;
            if (HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") != null)
            {
                List<ShoppingCarts> cartItem = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart");

                // Calculate total bill
                foreach (var item in cartItem)
                {
                    totalBill += item.bill;
                }
            }
            ViewData["TotalBill"] = totalBill;
            ViewData["ShippingAmount"] = 300.00m;
            ViewData["Promotion"] = promotion;
            //Show basket item
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cart.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            ViewBag.cart = cartItems;

            int? custid = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;

            var customer = _context.customers.FirstOrDefault(p => p.CustId == custid);
            var customerViewModel = new CustomerModelviese
            {
                CustId = customer?.CustId,
                Name = customer?.CustName,
                Email = customer?.CustEmail,
                password=customer.Password,
                number=customer.CustNumber,
                address=customer.Address
                // Populate other properties as needed
            };
            return View(customerViewModel);
        }
        [HttpPost]
        public IActionResult processShipping(string name, string email, string password, string number, string address, int PaymentTypeId,string phone,string cardNumberInput, string cvc, int expiryyear,int expirymonth, string cardType)
        {

            //var stripeSettings = _configuration.GetSection("StripeSettings").Get<StripeSettings>();
            //Stripe.StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            //ViewBag.stripeKey = _configuration["StripeSettings:PublishableKey"];
     
            // Show basket item
            List<ShoppingCarts> carts = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = carts.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            var payment = _context.paymentTypes.ToList();
            ViewBag.Payment = payment;
            List<ShoppingCarts> cart = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Check if customer already exists
            Customer cust = _context.customers.FirstOrDefault(c => c.CustEmail == email);

            if (cust != null)
            {
                name = cust.CustName;
                email = cust.CustEmail;
                password = cust.Password; // Note: Ideally, you shouldn't pre-fill the password
                number = cust.CustNumber;
                address = cust.Address;
            }
            Response.Cookies.Append("CustId", cust.CustId.ToString(), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMonths(1) // Set cookie expiration date (e.g., 1 month)
            });

            if (cust == null)
            {
                // Create new customer if not exists
                cust = new Customer
                {
                    CustName = name,
                    CustEmail = email,
                    Password = password,
                    CustNumber = number,
                    Address = address,
                    CreateDate = DateTime.Now,
                    LastModified = DateTime.Now
                };

                _context.customers.Add(cust);
                _context.SaveChanges();
            }

            Stripe.StripeConfiguration.ApiKey = _stripeSecretKey;

            //var options = new Stripe.PaymentIntentCreateOptions
            //{
            //    Amount = (long)((cart.Sum(c => c.bill) + 300) * 100), // amount in cents
            //    Currency = "usd",
            //    PaymentMethodTypes = new List<string> { "card" },
            //};

            //var service = new Stripe.PaymentIntentService();
            //Stripe.PaymentIntent paymentIntent = service.Create(options);

            //var payments = new Stripe.PaymentMethodCreateOptions
            //{
            //    Type = "card",
            //    Card = new Stripe.PaymentMethodCardOptions
            //   {
            //        Number = cardNumberInput,
            //        ExpMonth = expirymonth,
            //        ExpYear = expiryyear,
            //        Cvc = cvc,
            //    },
            //};
            //////var service = new Stripe.PaymentMethodService();
            //////Stripe.PaymentMethod paymentMethod = service.Create(payments);
            //var service = new Stripe.PaymentMethodService();
            //service.Create(payments);

            if (PaymentTypeId==2)
            {

                string token = cardNumberInput switch
                {
                    _ when cardNumberInput.StartsWith("4") => "tok_visa",          // Visa cards start with '4'
                    _ when cardNumberInput.StartsWith("5") => "tok_mastercard",    // Mastercard cards start with '5'
                    _ => "tok_visa"                                                // Default to Visa if not recognized
                };

                var cardTokenOptions = new Stripe.CardCreateOptions
                {
                    Source = token, // Use a test token from Stripe docs
                };
                var cardService = new Stripe.CardService();

                var customerOptions = new Stripe.CustomerCreateOptions
                {
                    Email = email,
                    Name = name,
                    Source = token,
                };
                var customerService = new Stripe.CustomerService();
                Stripe.Customer customer = customerService.Create(customerOptions);

                //create cardtokenid
                var card = cardService.Create(customer.Id, cardTokenOptions);

                var charges = new Stripe.ChargeCreateOptions
                {
                    Amount = (long)((cart.Sum(c => c.bill) + 300) * 100), // amount in cents
                    Currency = "usd",
                    Description = "Order Payment",
                    Source = token,
                };
                var chargesservice = new Stripe.ChargeService();
                Stripe.Charge charge = chargesservice.Create(charges);
            }

            if (cart != null)
            {
                    foreach (var item in cart)
                    {
                       _context.ShoppingCarts.Add(item);
                        Order od = new Order
                        {
                            veriationId = item.veriationId,
                            CustId = cust.CustId,
                            quantity = item.Quantity,
                            subtotal = item.bill,
                            shipping = 300,
                            totalamount = item.bill + 300,
                            Status = "Prosessing",
                            CreateDate = DateTime.Now,
                            Lastmodifield = DateTime.Now

                        };
                        _context.Orders.Add(od);
                        _context.SaveChanges();

                        Payment pay = new Payment
                        {
                            OrderId = od.OrderId,
                            CustId = cust.CustId,
                            totalamount = od.totalamount,
                            PaymentTypeId = PaymentTypeId,
                            PaymentStatus = "Paid",
                            CreateDate = DateTime.Now,
                            Lastmodifield = DateTime.Now
                        };
                        _context.payments.Add(pay);
                        _context.SaveChanges();
                        UpdateInventoryFromOrders();
                        SendOrderConfirmationEmail(cust, cart);
                    }

                    //_context.SaveChanges();
                    //UpdateInventoryFromOrders();
                    //SendOrderConfirmationEmail(cust, cart);
                }
                else
                {
                    ViewBag.Message = "please select product";
                }
            
                 return RedirectToAction("Index", "User");

        }
        [HttpPost]
        public async Task<IActionResult> Comments(int id, string price, string text, int rating)
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            var existingReview = _context.rewies.FirstOrDefault(r => r.productId == id && r.UserId == userId);

            if (existingReview != null)
            {
                ModelState.AddModelError("", "You have already submitted a review for this product.");
                return RedirectToAction("Index", "User");
            }

            if (rating > 5)
            {
                ModelState.AddModelError("", "Rating cannot be greater than 5.");
                return RedirectToAction("Index", "User");
            }
            var Review = new Rewies();
            Review.productId = id;
            Review.comment = text;
            Review.Rating = rating;
            Review.UserId = userId;
            Review.CreateDate = DateTime.Now;
            Review.Lastmodifield = DateTime.Now;
            _context.rewies.Add(Review);
            _context.SaveChanges();
            return RedirectToAction("ProductDetails", "User");
        }
        public IActionResult ViewAll(int min, int max, string term = "")
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }

            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cartItems.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;

            var colorCounts = _context.productVeriations
             .GroupBy(p => p.productcolorId)
            .Select(g => new { ColorId = g.Key, Count = g.Count() })
           .Join(_context.productColors, // Assuming the name of your color table is "Colors"
              pv => pv.ColorId,
             c => c.productcolorId,
             (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
            .ToList();
            ViewBag.ColorCounts = colorCounts;

            var totalcolor = _context.productColors.Count();
            ViewBag.productcolor = totalcolor;

            var totalsize = _context.productSizes.Count();
            ViewBag.productsize = totalsize;

            var totalprice = _context.product.Select(p => p.price).Sum();
            ViewBag.totalprice = totalprice;

            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;

            var maximun = _context.product.Max(p => p.price);
            ViewBag.maximun = maximun;

            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();
            ViewBag.sizeCounts = sizeCounts;

            var fabric = _context.productMaterials.Count();
            ViewBag.fabriccount = fabric;

            var fabriclist = _context.productMaterials
            .GroupBy(p => p.productmaterialId)
             .Select(g => new { materialId = g.Key, Count = g.Count() })
             .Join(_context.productMaterials,
              pv => pv.materialId,
               c => c.productmaterialId,
              (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

            .ToList();
            ViewBag.fabriccounts = fabriclist;

            ViewBag.cat = _context.categories.Count();

            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();
            ViewBag.CatCounts = catCounts;

            var productsWithDiscounts = _context.product
      .Select(p => new
      {
          p.productId,
          p.productName,
          p.productimage,
          p.price,
          Discount = _context.promotionmappings
                      .Where(pm => pm.productId == p.productId)
                      .Select(pm => pm.Promotions.Value)
                      .FirstOrDefault(),
          costprice = (p.price - (p.price * (_context.promotionmappings
                                            .Where(pm => pm.productId == p.productId)
                                            .Select(pm => (double?)pm.Promotions.Value)
                                            .FirstOrDefault() ?? 0) / 100)),

      }).ToList();

            ViewBag.data = productsWithDiscounts;
            return View();
        }
        public IActionResult Getcolorimages(int[] colors, int min, int max, string term, int currentpage = 1)
        {
            string discountValueStr = HttpContext.Session.GetString("discountValue");
            //////// Convert the discount value back to an integer
            int discountValue = int.Parse(discountValueStr);

            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }

            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //if (userId.HasValue)
            //{
            //    var user = _context.signups.FirstOrDefault(p => p.Id == userId);
            //    var Name = user.Email;
            //    ViewBag.UserName = Name;
            //}
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();
            // Calculate the count of cart items
            int cartItemCount = cartItems.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;

            var totalcolor = _context.productColors.Count();
            ViewBag.productcolor = totalcolor;
            var totalsize = _context.productSizes.Count();
            ViewBag.productsize = totalsize;

            var color = _context.productColors.ToList();
            var size = _context.productSizes.ToList();
            ViewBag.size = size;
            var colorCounts = _context.productVeriations
             .GroupBy(p => p.productcolorId)
            .Select(g => new { ColorId = g.Key, Count = g.Count() })
           .Join(_context.productColors,
            pv => pv.ColorId,
             c => c.productcolorId,
             (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
            .ToList();
            ViewBag.ColorCounts = colorCounts;
            var totalprice = _context.product.Select(p => p.price).Sum();
            ViewBag.totalprice = totalprice;

            var maximun = _context.product.Select(p => p.price).Max();
            ViewBag.maximun = maximun;

            var products = _context.productVeriations
             .Include(p => p.Product)
            .Where(p => colors.Contains(p.productcolorId))
             .ToList();
            ViewBag.products = products;

            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();
            ViewBag.sizeCounts = sizeCounts;
            var filteredImages = _context.productVeriations
         .Include(p => p.Product)
         .Where(img => img.costprice >= min && img.costprice <= max)
         .ToList();
            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;
            var fabriclist = _context.productVeriations.Include(p => p.ProductMaterial).Include(p => p.Product)
        .GroupBy(p => p.productmaterialId)
         .Select(g => new { materialId = g.Key, Count = g.Count() })
         .Join(_context.productMaterials,
          pv => pv.materialId,
           c => c.productmaterialId,
          (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

        .ToList();
            ViewBag.fabriccounts = fabriclist;
            ViewBag.cat = _context.categories.Count();
            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();
            ViewBag.CatCounts = catCounts;

            colors ??= new int[0]; // Ensure size is not null
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

            //var catdata = new Checkcolorsize();
            //ager discount nhi ho ga toh me sirf StartsWith tak kam karo gye
            var cat = _context.productVeriations
           .Include(pv => pv.Product) // Include associated product
           .Where(pv => colors.Length == 0 || colors.Contains(pv.productcolorId)) // Filter based on selected colors
           .Where(pv => term == "" || pv.Product.productName.ToLower().StartsWith(term)) // Filter based on term
           .Select(pv => new ProductVeriation
           {
               productId = pv.productId,
               image = pv.image,            
               Product = new Product
               {
                   productId = pv.Product.productId,
                   productName = pv.Product.productName,
                   price = pv.Product.price - (pv.Product.price * discountValue / 100),

                   Discount = _context.promotionmappings
                      .Where(pm => pm.productId == pv.productId)
                      .Select(pm => pm.Promotions.Value)
                        .FirstOrDefault(),     
               }
           });
            int totalrecord = cat.Count();
            int pagesize = 5;
            int totalpages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            cat = cat.Skip((currentpage - 1) * pagesize).Take(pagesize); // Convert to list here
            var catdata = new Checkcolorsize();
            catdata.productVeriations = cat;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totalpages;
            catdata.term = term;          
            return View(catdata);
        }
        public IActionResult Getszeimages(int[] size, int min, int max, string term, int currentpage = 1)
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;

            //if (userId.HasValue)
            //{
            //    var user = _context.signups.FirstOrDefault(p => p.Id == userId);

            //    var Name = user.Email;
            //    ViewBag.UserName = Name;
            //}

            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cartItems.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;

            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;

            var totalcolor = _context.productColors.Count();
            ViewBag.productcolor = totalcolor;
            var totalsize = _context.productSizes.Count();
            ViewBag.productsize = totalsize;

            var productcolor = _context.productColors.Count();
            ViewBag.productcolor = productcolor;

            var color = _context.productColors.ToList();
            var sizes = _context.productSizes.ToList();
            ViewBag.size = sizes;

            var colorCounts = _context.productVeriations
            .GroupBy(p => p.productcolorId)
           .Select(g => new { ColorId = g.Key, Count = g.Count()})
             .Join(_context.productColors,
              pv => pv.ColorId,
            c => c.productcolorId,
            (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
           .ToList();
            ViewBag.ColorCounts = colorCounts;

            var totalprice = _context.product.Select(p => p.price).Sum();
            ViewBag.totalprice = totalprice;

            var maximun = _context.product.Select(p => p.price).Max();
            ViewBag.maximun = maximun;

            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();
            ViewBag.sizeCounts = sizeCounts;
            var sizecount = _context.productVeriations.Include(p => p.Product)
            .Where(p=>size.Contains(p.productsizeId));
            ViewBag.sizecount = sizecount;

            var filteredImages = _context.productVeriations
         .Include(p => p.Product)
         .Where(img => img.costprice >= min && img.costprice <= max)
         .ToList();
            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;

            var maximuns = _context.product.Max(p => p.price);
            ViewBag.maximun = maximuns;

            var fabriclist = _context.productVeriations.Include(p => p.ProductMaterial).Include(p => p.Product)
           .GroupBy(p => p.productmaterialId)
            .Select(g => new { materialId = g.Key, Count = g.Count() })
            .Join(_context.productMaterials,
             pv => pv.materialId,
              c => c.productmaterialId,
             (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

           .ToList();
            ViewBag.fabriccounts = fabriclist;

            ViewBag.cat = _context.categories.Count();

            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();

            ViewBag.CatCounts = catCounts;
            size ??= new int[0]; // Ensure size is not null
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            //var catdata = new Checkcolorsize();
            var selectsize = _context.productVeriations.Include(p => p.Product)
            .Where(p => size.Length == 0 || size.Contains(p.productsizeId))
            .Where(emp => term == "" || emp.Product.productName.ToLower().StartsWith(term))
            // Filter based on term
              .Select(pv => new ProductVeriation
              {
                  productId = pv.productId,
                  image = pv.image,
                  costprice = pv.Product.price,
                  Product = new Product
                  {
                      productName = pv.Product.productName,
                      Discount = _context.promotionmappings
                      .Where(pm => pm.productId == pv.productId)
                      .Select(pm => pm.Promotions.Value)
                        .FirstOrDefault(),
                  }

              });
            int totalrecord = selectsize.Count();
            int pagesize = 4;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            selectsize = selectsize.Skip((currentpage - 1) * pagesize).Take(pagesize);
            var catdata = new Checkcolorsize();
            catdata.productVeriations = selectsize;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            catdata.term = term;
            return View(catdata);
        }
        public IActionResult Getpriceimages(int min, int max, int id, string term = "")
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }

            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;

            //if (userId.HasValue)
            //{
            //    var user = _context.signups.FirstOrDefault(p => p.Id == userId);
            //    var Name = user.Email;
            //    ViewBag.UserName = Name;
            //}
            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;
            List<ShoppingCarts> cartItems = HttpContext.Session.GetObject<List<ShoppingCarts>>("Cart") ?? new List<ShoppingCarts>();

            // Calculate the count of cart items
            int cartItemCount = cartItems.Sum(item => item.Quantity);

            // Store the count in ViewBag
            ViewBag.CartItemCount = cartItemCount;
            var color = _context.productColors.ToList();
            ViewBag.color = color;
            var size = _context.productSizes.ToList();
            ViewBag.size = size;

            var colorCounts = _context.productVeriations
             .GroupBy(p => p.productcolorId)
            .Select(g => new { ColorId = g.Key, Count = g.Count() })
           .Join(_context.productColors, // Assuming the name of your color table is "Colors"
              pv => pv.ColorId,
             c => c.productcolorId,
             (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
            .ToList();
            ViewBag.ColorCounts = colorCounts;

            var totalcolor = _context.productColors.Count();
            ViewBag.productcolor = totalcolor;

            var totalsize = _context.productSizes.Count();
            ViewBag.productsize = totalsize;

            var totalprice = _context.product.Select(p => p.price).Sum();
            ViewBag.totalprice = totalprice;
       
            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();

            ViewBag.sizeCounts = sizeCounts;

            //priceset
            var filteredImages = _context.productVeriations
            .Include(p => p.Product)
            .Where(img => img.costprice >= min && img.costprice <= max)
            .ToList();

            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;

            var maximun = _context.product.Select(p => p.price).Max();
            ViewBag.maximun = maximun;


            var fabriclist = _context.productVeriations.Include(p => p.ProductMaterial).Include(p => p.Product)
                .GroupBy(p => p.productmaterialId)
            .Select(g => new { materialId = g.Key, Count = g.Count() })
            .Join(_context.productMaterials,
             pv => pv.materialId,
             c => c.productmaterialId,
            (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

         .ToList();
            ViewBag.fabriccounts = fabriclist;

            ViewBag.cat = _context.categories.Count();

            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();
            ViewBag.CatCounts = catCounts;

            //var products = _context.productVeriations
            //           .Include(p => p.Product)
            //          .Where(p => price.Contains(p.productId))
            //           .ToList();

            return View(filteredImages);
        }
        public IActionResult Getmaterialimages(int[] fabricid, int min, int max, string term = "", int currentpage = 1)
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }


            var categories = _context.categories.ToList();
            ViewBag.Categories = categories;

            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;

            var maximun = _context.product.Select(p => p.price).Max();
            ViewBag.maximun = maximun;

            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();
            ViewBag.sizeCounts = sizeCounts;

            var colorCounts = _context.productVeriations
               .GroupBy(p => p.productcolorId)
              .Select(g => new { ColorId = g.Key, Count = g.Count() })
             .Join(_context.productColors,
              pv => pv.ColorId,
               c => c.productcolorId,
               (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
              .ToList();
            ViewBag.ColorCounts = colorCounts;

            ViewBag.fabriccount = _context.productMaterials.Count();

            var fabriclist = _context.productVeriations.Include(p=>p.ProductMaterial).Include(p=>p.Product)
            .GroupBy(p => p.productmaterialId)
             .Select(g => new { materialId = g.Key, Count = g.Count() })
             .Join(_context.productMaterials,
              pv => pv.materialId,
               c => c.productmaterialId,
              (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

            .ToList();
            ViewBag.fabriccounts = fabriclist;
            ViewBag.cat = _context.categories.Count();

            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();
            ViewBag.CatCounts = catCounts;

            var filteredImages = _context.productVeriations
          .Include(p => p.Product)
          .Where(img => img.costprice >= min && img.costprice <= max)
          .ToList();

            fabricid ??= new int[0]; // Ensure size is not null

            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            //var catdata = new Checkcolorsize();

            var material = _context.productVeriations.Include(p => p.Product)
            .Where(p => fabricid.Length == 0 || fabricid.Contains(p.productmaterialId)) // Filter based on selected sizes
            .Where(emp => term == "" || emp.Product.productName.ToLower().StartsWith(term))
                  .Select(pv => new ProductVeriation
                  {
                      productId = pv.productId,
                      image = pv.image,
                      costprice = pv.Product.price,
                      Product = new Product
                      {
                          productName = pv.Product.productName,
                          Discount = _context.promotionmappings
                      .Where(pm => pm.productId == pv.productId)
                      .Select(pm => pm.Promotions.Value)
                        .FirstOrDefault(),
                      }

                  });// Filter based on term

            int totalrecord = material.Count();
            int pagesize = 4;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            material = material.Skip((currentpage - 1) * pagesize).Take(pagesize);

            var catdata = new Checkcolorsize();
            catdata.productVeriations = material;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            catdata.term = term;
            return View(catdata);
        }
        public IActionResult GetCategory(int[] cat, int min, int max, string term = "", int currentpage = 1)
        {
            string userId = HttpContext.Request.Cookies["UserId"];


            //int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            //int? userId = HttpContext.Session.GetInt32("UserId");
            /////int? userId = TempData["UserId"] as int?;  

            if (userId != null)
            {
                var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
                var Name = user.Email;
                ViewBag.UserName = Name;
            }


            ViewBag.cat = _context.categories.Count();
            var catCounts = _context.productVeriations
          .GroupBy(p => p.categoryId)
         .Select(g => new { catId = g.Key, Count = g.Count() })
        .Join(_context.categories, // Assuming the name of your color table is "Colors"
           pv => pv.catId,
          c => c.categoryId,
          (pv, c) => new { catId = pv.catId, CatName = c.categoryName, Count = pv.Count })
         .ToList();
            ViewBag.CatCounts = catCounts;

            var mins = _context.product.Select(p => p.price).Min();
            ViewBag.min = mins;

            var maximun = _context.product.Select(p => p.price).Max();
            ViewBag.maximun = maximun;

            var sizeCounts = _context.productVeriations
            .GroupBy(p => p.productsizeId)
            .Select(g => new { SizeId = g.Key, Count = g.Count() })
            .Join(_context.productSizes,
             pv => pv.SizeId,
              c => c.productsizeId,
             (pv, c) => new { SizeId = pv.SizeId, SizeName = c.productsizeName, Count = pv.Count })
            .ToList();
            ViewBag.sizeCounts = sizeCounts;

            var colorCounts = _context.productVeriations
               .GroupBy(p => p.productcolorId)
              .Select(g => new { ColorId = g.Key, Count = g.Count() })
             .Join(_context.productColors,
              pv => pv.ColorId,
               c => c.productcolorId,
               (pv, c) => new { ColorId = pv.ColorId, ColorName = c.productcolorName, Count = pv.Count })
              .ToList();
            ViewBag.ColorCounts = colorCounts;

            ViewBag.fabriccount = _context.productMaterials.Count();

            var fabriclist = _context.productVeriations.Include(p => p.ProductMaterial).Include(p => p.Product)
            .GroupBy(p => p.productmaterialId)
             .Select(g => new { materialId = g.Key, Count = g.Count() })
             .Join(_context.productMaterials,
              pv => pv.materialId,
               c => c.productmaterialId,
              (pv, c) => new { materialId = pv.materialId, MaterialName = c.productmaterialname, Count = pv.Count })

            .ToList();
            ViewBag.fabriccounts = fabriclist;
            ViewBag.cat = _context.categories.Count();

            cat ??= new int[0]; // Ensure size is not null
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            //var catdata = new Checkcolorsize();

            var category = _context.productVeriations.Include(p => p.Product)
            .Where(p => cat.Length == 0 || cat.Contains(p.categoryId)) // Filter based on selected sizes
            .Where(emp => term == "" || emp.Product.productName.ToLower().StartsWith(term))
                  .Select(pv => new ProductVeriation
                  {
                      productId = pv.productId,
                      image = pv.image,    
                      Product = new Product
                      {
                          productName = pv.Product.productName,
                          price=pv.Product.price,
                          Discount = _context.promotionmappings
                      .Where(pm => pm.productId == pv.productId)
                      .Select(pm => pm.Promotions.Value)
                        .FirstOrDefault(),
                      }

                  });// Filter based on term// Filter based on term

            int totalrecord = cat.Count();
            int pagesize = 5;
            int totlapages = (int)Math.Ceiling(totalrecord / (double)pagesize);
            category = category.Skip((currentpage - 1) * pagesize).Take(pagesize);

            var catdata = new Checkcolorsize();
            catdata.productVeriations = category;
            catdata.currentpage = currentpage;
            catdata.pagesize = pagesize;
            catdata.totalpage = totlapages;
            catdata.term = term;
            return View(catdata);
        }
        public void UpdateInventoryFromOrders()
        {
            var orders = _context.Orders.ToList();
            var qty = 10;

            foreach (var order in orders)
            {
                // Check if the order already exists in the inventory
                bool orderExistsInInventory = _context.inventories.Any(i => i.OrderId == order.OrderId);

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
            }
            _context.SaveChanges();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();

        }
        [HttpPost]
        //userordercancelled
        public IActionResult Contact(int id)
        {
            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;
            var order = _context.Orders.Where(i => i.CustId == userId).FirstOrDefault();
            DateTime twoDaysAgo = DateTime.Now.AddDays(-2).Date;

            // Find the order placed exactly two days ago by the user
            var orders = _context.Orders
                                .Where(i => i.CustId == userId && i.CreateDate.Date == twoDaysAgo)
                                .FirstOrDefault();
            //var contact = new ContactUs
            //{
            //    Description = text,
            //    OrderId = orders.OrderId,
            //};
            //_context.contactUs.Add(contact);
            //_context.SaveChanges();
            OrderCancelledEmail();
          return View();
        }
        //sendemailadmin
        private void OrderCancelledEmail()
        {
            int? userId = Request.Cookies["CustId"] != null ? int.Parse(Request.Cookies["CustId"]) : (int?)null;
            var order = _context.Orders.Where(i => i.CustId == userId).FirstOrDefault();

            var email = "rachnasantosh7@gmail.com";

            string emailBody = "My order cancelled \n\n";
            emailBody += "\n\nRegards,\nYour Company \n\n";
            
            emailBody += $"customerId: {userId}, OrderNumber: {order.OrderId}\n";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(email);
                mail.Subject = "User Order Cancelled";
                mail.Body = emailBody;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential Credentials = new NetworkCredential(email, "jffjdvdizbobtjpw");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = Credentials;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
        }     
        //contactuspaye
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(string name, string email, string sub, string msg)
        {
            int? userId = Request.Cookies["UserId"] != null ? int.Parse(Request.Cookies["UserId"]) : (int?)null;
            var user = _context.signups.FirstOrDefault(p => p.Id == userId);
            var users = _context.signups.Any(p => p.Name == name && p.Email == email);

            contact con= new contact
            {
                Id = userId.Value,
                Subject = sub,
                Comment = msg

            };
            _context.contacts.Add(con);
            _context.SaveChanges();

            return RedirectToAction("Index","User");
        }
        public IActionResult Customize()
        {
            return View();
        }

        public IActionResult Uploadimage()
        {        
            return View();
        }
    }

}


