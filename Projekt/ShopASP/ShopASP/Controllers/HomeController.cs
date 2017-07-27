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
            var newItems = db.Items.Where(a => !a.IsHidden).OrderByDescending(a => a.CreateDate).Take(3).ToList();

            var bestsellers = db.Items.Where(a => !a.IsHidden && a.IsHidden).OrderBy(g => Guid.NewGuid()).Take(3).ToList();

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
    }
}