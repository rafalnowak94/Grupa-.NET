using ShopASP.DAL;
using ShopASP.Infastructure;
using ShopASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopASP.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionFuncs sessionManager { get; set; }
        private ShopContext db = new ShopContext();
        public CartController()
        {
            this.sessionManager = new defaultSession();
            this.cartManager = new CartManager(this.sessionManager, this.db);
        }
        // GET: Cart

        public ActionResult AddToCart(int id)
        {
            //cartManager.AddToCart(id);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Index()
        {
            // cartItems = cartManager.GetCart();
            // var cartTotalPrice = cartManager.GetCartTotalPrice();

            // CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };

            return View();
        }
    }
}