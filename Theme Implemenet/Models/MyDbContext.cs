using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Theme_Implemenet.Models
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Register> registers { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Module> modules { get; set; }
        public DbSet<Pages> pages { get; set; }
        public DbSet<Modulepagespermissions> modulepagespermissions { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subcategories { get; set; }
        public DbSet<CategoryStyle> categoriestyle { get; set; }
        public DbSet<ProductSeason> productSeasons { get; set; }
        public DbSet<Productgender> productgenders { get; set; }
        public DbSet<ProductMaterial> productMaterials { get; set; }
        public DbSet<ProductSize> productSizes { get; set; }
        public DbSet<ProductColor> productColors { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<ProductVeriation> productVeriations { get; set; }
        public DbSet<userproductpermission> userproductpermissions { get; set; }
        public DbSet<ProductImage> productImages {get; set;}
        public DbSet<Customer> customers {get; set;}
        public DbSet<Payment> payments { get; set;}
        public DbSet<Inventory> inventories { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<ShoppingCarts> ShoppingCarts {get; set;}
        public DbSet<PaymentType> paymentTypes {get; set;}    
        public DbSet<Signup> signups { get; set;}
        public DbSet<Rewies> rewies { get; set; }
        public DbSet<OderStatus> OderStatus { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<contact> contacts {get; set;}
        public DbSet<Promotions> promotions { get; set; }
        public DbSet<Promotionmapping> promotionmappings {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                            .HasOne(u => u.Status)
                             .WithMany(s => s.Users)
                              .HasForeignKey(u => u.StatusId)
                                .IsRequired(false);

            modelBuilder.Entity<ApplicationUser>()
                 .HasOne(u => u.Company)
                  .WithMany(s => s.Users)
                   .HasForeignKey(u => u.CompanyId)
                     .IsRequired(false);

            modelBuilder.Entity<ApplicationRole>()
           .ToTable("AspNetRoles");

            modelBuilder.Entity<ApplicationRole>()
            .HasOne(u => u.Company)
             .WithMany(s => s.Roles)
              .HasForeignKey(u => u.CompanyId)
                .IsRequired(false);

            //modelBuilder.Entity<ApplicationRole>()
            //       .HasOne(u => u.Status)
            //        .WithMany(s => s.Role)
            //         .HasForeignKey(u => u.StatusId)
            //           .IsRequired(false);

                   modelBuilder.Entity<userproductpermission>()
            .HasOne(up => up.ApplicationRole)
            .WithMany(u => u.userproductpermissions)
           .HasForeignKey(up => up.RoleId)
            .IsRequired(false);

                 //modelBuilder.Entity<userproductpermission>()
                 //          .HasOne(up => up.ApplicationUser)
                 //          .WithMany(r => r.userproductpermissions)
                 //          .HasForeignKey(up => up.Id)
                 //          .IsRequired(false);

            modelBuilder.Entity<Modulepagespermissions>()
                     .HasOne(mpp => mpp.ApplicationRole)
                     .WithMany(ar => ar.Modulepagespermissions)
                     .HasForeignKey(mpp => mpp.RoleId)
                     .IsRequired(false);

            modelBuilder.Entity<ShoppingCarts>()
               .HasOne(u => u.ApplicationUser)
                .WithMany(s => s.ShoppingCarts)
                 .HasForeignKey(u => u.UserId)
                   .IsRequired(false);

            modelBuilder.Entity<Rewies>()
             .HasOne(p => p.ApplicationUser)
              .WithMany(c => c.Rewies)
               .HasForeignKey(p => p.UserId)
                 .IsRequired(false);
        }
    }

}