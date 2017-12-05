using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SampleAuth1.DBService;
using SampleAuth1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SampleAuth1.Controllers
{
    public class UsercategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Usercategories
        public ActionResult Index()
        {
            String ApplicationUser_Id = User.Identity.GetUserId();
         
            List<Category> Categories = (from category in db.Categories
                        select category).ToList<Category>();

            
            List<UserCategories> currentUserCategories = (from uc in db.UserCategories
                                                          where uc.ApplicationUser_Id == ApplicationUser_Id
                                                          select uc).ToList<UserCategories>();
            if (currentUserCategories.Count !=0)
            {
                foreach (UserCategories UserCategories in currentUserCategories)
                {
                    foreach (Category category in Categories)
                    {
                        if (category.CategoryId == UserCategories.CategoryId)
                        {
                            category.Selected = true;
                        }
                    }
                }
            }

            if (Categories == null)
            {
                return HttpNotFound();
            }
            return View(Categories);
            
        }

        public ActionResult getCurrentUserCategories()
        {
            var list = (from category in db.Categories
                        select category).ToList();
            if (list == null)
            {
                return HttpNotFound();
            }
            
            String ApplicationUser_Id = User.Identity.GetUserId();
            List<UserCategories> currentUserCategories = (from uc in db.UserCategories
                                         where uc.ApplicationUser_Id == ApplicationUser_Id
                                         select uc).ToList<UserCategories>();
            Int64[] userCategories = new Int64[currentUserCategories.Count];
            for (int i =0; i < currentUserCategories.Count; i++)
            {

                userCategories[i] = currentUserCategories[i].CategoryId;
            }



            // string json_data = JsonConvert.SerializeObject(userCategories);

            //var result = new { Success = "true", Message = "Success" };
            //return Json(userCategories);

            
            return Json(userCategories, JsonRequestBehavior.AllowGet);

        }







        // POST: Usercategories/Create
        [HttpPost]
        public ActionResult saveUserCategories(Int64[] categories)
        {
            if (categories.Length != 0 && User.Identity.GetUserId() != null)
            {

                //Remove Exsisting Category selection
                String ApplicationUser_Id = User.Identity.GetUserId();
                var currentUserCategories = (from uc in db.UserCategories
                                                              where uc.ApplicationUser_Id == ApplicationUser_Id
                                                              select uc).ToList();
                db.UserCategories.RemoveRange(currentUserCategories);
                db.SaveChanges();

                //Update new Category selection
                updateUserCategories(categories);
            }
            
            var result = new { Success = "true", Message = "Categories Selection Updated..." };
            return Json(result);

        }

        private void updateUserCategories(Int64[] categories)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                UserCategories userCategory = new UserCategories()
                {
                    CategoryId = categories[i],
                    ApplicationUser_Id = User.Identity.GetUserId()
                };
                db.UserCategories.Add(userCategory);
                db.SaveChanges();
            }
            
        }
 
    }
}
