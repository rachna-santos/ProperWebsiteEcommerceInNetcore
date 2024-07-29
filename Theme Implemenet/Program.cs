using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using System.Configuration;
using System.Net;
using Theme_Implemenet.Models;
using Stripe;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.CodeAnalysis.Scripting;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1); // Session timeout set to 1 day
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<MyDbContext>();
builder.Services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

// Set Stripe API Key from configuration
//StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];




//Googleauthentication
//builder.Services.AddAuthentication(option =>
//{
//    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;


//})
// .AddCookie()
//.AddGoogle(GoogleDefaults.AuthenticationScheme, option =>
//    {
//        option.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//        option.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//        option.AccessType = builder.Configuration["Authentication:Google:AccessType"];
//        option.ClaimActions.MapJsonKey("urn:google:picture","picture","url");

//    });


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
