using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleAuth1.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Index()
        {
            //Console.WriteLine("");
            return RedirectToAction("Index", "Usercategories");
        }
    }
}