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
            var item = db.Items.Find(id);
            return View(item);
        }
        public ActionResult List(string categoryname, string searchQuery = null)
        {
            var category = db.Categories.Include("Items").Where(g => g.Name.ToLower() == categoryname.ToLower()).Single();
            var items = category.Items.Where(a=> (searchQuery ==null ||
                                              a.Title.ToLower().Contains(searchQuery.ToLower()))&&
                                              !a.IsHidden);
            if(Request.IsAjaxRequest())
            {
                return PartialView("_ItemsList", items);
            }
            return View(items);
        }

        [ChildActionOnly]
        public ActionResult GenerateCategoriesMenuFull()
        {
            var categories = db.Categories.ToList();
            return PartialView("_CategoriesMenuFull",categories);
        }

        public ActionResult ItemsSuggestions(string term)
        {
            var items = db.Items.Where(a => !a.IsHidden && a.Title.ToLower().Contains(term.ToLower()))
                  .Take(8).Select(a => new { label = a.Title });

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}