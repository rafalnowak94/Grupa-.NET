using ShopASP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopASP.Controllers
{
    public class ShopController : Controller
    {
        ShopContext db = new ShopContext();
        // GET: Shop
        public ActionResult Description(int id)
        {
            return View();
        }
        public ActionResult List(string categoryname)
        {
            var category = db.Categories.Include("Items").Where(g => g.Name.ToLower() == categoryname.ToLower()).Single();
            var items = category.Items.ToList();
            return View(items);
        }

        [ChildActionOnly]
        public ActionResult GenerateCategoriesMenu()
        {
            var categories = db.Categories.ToList();
            return PartialView("_CategoriesMenu",categories);
        }
    }
}