using SampleAuth1.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SampleAuth1.Controllers
{
    public class ProductAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProductAdmin
        public ActionResult Index()
        {
            ViewBag.Message = "This is Product Admin Page";
            return View();
        }
        public ActionResult ManageProducts()
        {

            
            return View();
        }
        public ActionResult ManageCategory()
        {
            var list = (from category in db.Categories
                                                select category).ToList();
            if (list == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(list);
            
        }

    }
}
