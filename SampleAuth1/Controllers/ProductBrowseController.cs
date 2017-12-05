using SampleAuth1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SampleAuth1.DBService;
using System.Data.Entity.Validation;
using SampleAuth1.Utils;

namespace SampleAuth1.Controllers
{
    public class ProductBrowseController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: ProductBrowse/Index
        public ActionResult Index()
        {
            String ApplicationUser_Id = User.Identity.GetUserId();
            if (ApplicationUser_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            List<Product> totalProductList = (from product in db.Products
                                              where
                                              (from uc in db.UserCategories
                                               where uc.ApplicationUser_Id == ApplicationUser_Id
                                               select uc.CategoryId).Contains(product.CategoryId)
                                              select product).ToList<Product>();
            List<Product> wishlist = getWishList();
            List<Product> scraplist = getScrapList();
            List<Product> notviewedproductlist = new List<Product>();


            foreach (Product product in wishlist)
            {
                totalProductList.Remove(product);
            }

            foreach (Product product in scraplist)
            {
                totalProductList.Remove(product);
            }

            if (totalProductList.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Random random = new Random();
                int rIndex = random.Next(0, totalProductList.Count);
                var randomList = new List<Product>();

                randomList.Add(totalProductList[rIndex]);


                return View(randomList);
            }


        }

        //Adding to WishList
        public ActionResult AddToFav(int pid)
        {


            WishList wl = new WishList()
            {

                ApplicationUser_Id = User.Identity.GetUserId(),
                Product_Id = pid
            };

            db.WishLists.Add(wl);

            db.SaveChanges();


            return RedirectToAction("Index");
        }
        //Adding to scrap list
        public ActionResult notFav(int pid)
        {
            ScrapList sl = new ScrapList()
            {
                ApplicationUser_Id = User.Identity.GetUserId(),
                Product_Id = pid
            };

            db.ScrapLists.Add(sl);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ProductBrowse/Favorites
        public ActionResult WishList()
        {
            List<Product> list = getWishList();
            if (list == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View("WishList", list);

        }

        private List<Product> getWishList()
        {
            String ApplicationUser_Id = User.Identity.GetUserId();
            var productIdList = (from wl in db.WishLists where wl.ApplicationUser_Id == ApplicationUser_Id select wl.Product_Id);
            List<Product> wishlist = (from p in db.Products where productIdList.Contains(p.Id) select p).ToList<Product>();
            return wishlist;
        }

        private List<Product> getScrapList()
        {
            String ApplicationUser_Id = User.Identity.GetUserId();
            var productIdList = (from sl in db.ScrapLists where sl.ApplicationUser_Id == ApplicationUser_Id select sl.Product_Id);
            List<Product> scraplist = (from p in db.Products where productIdList.Contains(p.Id) select p).ToList<Product>();
            return scraplist;
        }

        public ActionResult RemoveFromWishList(int pid)
        {
            WishList removeItem = (from wl in db.WishLists
                                   where wl.Product_Id == pid
                                   select wl).FirstOrDefault();


            db.WishLists.Remove(removeItem);

            db.SaveChanges();

            List<Product> list = getWishList();
            if (list == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View("WishList", list);
        }
        // Tosend email 
        public ActionResult sendEmail(String ToEmail)
        {
            EmailClient ec = new EmailClient();
            List<Product> products = getWishList();
            ec.sendEmail(ToEmail, products);
            return View("EmailConfirmation");

        }
    }
}