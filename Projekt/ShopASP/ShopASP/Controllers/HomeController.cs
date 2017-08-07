using ShopASP.DAL;
using ShopASP.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;


namespace ShopASP.Controllers
{
    public class HomeController : Controller
    {
        private ShopContext db = new ShopContext();
        // GET: Home
        public ActionResult Index()
        {
            // retrieving three new items from database 
            var newItems = db.Items.Where(a => !a.IsHidden).OrderByDescending(a => a.CreateDate).Take(3).ToList();

            // retrieving three(random because it's order by guid) bestsellers items from database 
            var bestsellers = db.Items.Where(a => !a.IsHidden && a.IsBestseller).OrderBy(g => Guid.NewGuid()).Take(3).ToList();

            // save items in HomeViewModel
            var vm = new HomeViewModel()
            {
                NewItems = newItems,
                Bestsellers = bestsellers
            };

            return View(vm);
        }

        public ActionResult StaticPage(string page)
        {
            return View(page);
        }

        //we can use it only in code
        [ChildActionOnly]
        public ActionResult GenerateCategoriesMenu()
        {
            var categories = db.Categories.ToList();
            return PartialView("_CategoriesMenu", categories);
        }
    }
}