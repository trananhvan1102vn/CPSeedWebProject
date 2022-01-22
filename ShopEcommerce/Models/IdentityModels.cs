using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CPSeed.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationRole : IdentityRole {
        public virtual IList<Menu> Menus { get; set; }
    }
    public class ApplicationUser : IdentityUser {
        [Display(Name = "Ảnh đại diện")]
        public string Photo { get; set; }
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Ngày tham gia")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }

        public virtual IList<ActivityLog> ActivityLogs { get; set; }
        public virtual IList<LoginSession> LoginSessions { get; set; }

        public ApplicationUser() {
            ActivityLogs = new List<ActivityLog>();
            LoginSessions = new List<LoginSession>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context) {
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser, string> {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
            : base(store) {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> {
        public ApplicationDbContext()
            : base("DefaultConnection") {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BannerCategory> BannerCategories { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<LoginSession> LoginSessions { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        

        public virtual DbSet<ProductViewer> ProductViewers { get; set; }
        public virtual DbSet<RequestProduct> RequestProducts { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<VisitorCounter> VisitorCounters { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }
        public virtual DbSet<CodeTable> CodeTables { get; set; }
        public virtual DbSet<KeyData> KeyDatas { get; set; }

        public virtual DbSet<ConfigSetting> ConfigSettings { get; set; }

        public virtual DbSet<Plugin> Plugins { get; set; }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}