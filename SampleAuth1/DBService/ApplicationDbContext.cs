using Microsoft.AspNet.Identity.EntityFramework;
using SampleAuth1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SampleAuth1.DBService
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()  : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<ScrapList> ScrapLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategories> UserCategories { get; set; }
    }
}
