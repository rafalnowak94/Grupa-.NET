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
    public class BasketController : Controller
    {
        private CartManager cartManager;

        private ISessionFuncs sessionManager;

        private ShopContext db;

        public BasketController()
        {
            db = new ShopContext();
            sessionManager = new defaultSession();
            cartManager = new CartManager(sessionManager, db);
        }
        // GET: Basket
        public ActionResult Index()
        {
            var cartItems = cartManager.GetCart();
            var cartTotalPrice = cartManager.GetCartTotalPrice();

            CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };
            return View(cartVM);
        }

        public ActionResult AddToBasket(int id)
        {
            cartManager.AddToCart(id);
            return RedirectToAction("Index", "Basket");
        }

        public int GetCartItemCount()
        {
            return cartManager.GetCartItemsCount();
        }

        public ActionResult RemoveFromBasket(int itemid)
        {
            CartManager CartManager = new CartManager(this.sessionManager, this.db);

            int itemCount = CartManager.RemoveFromCart(itemid);
            int cartItemsCount = CartManager.GetCartItemsCount();
            decimal cartTotal = CartManager.GetCartTotalPrice();

            // Return JSON to process it in JavaScript
            var result = new BasketRemoveViewModel
            {
                RemoveItemId = itemid,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount
            };

            return Json(result);
        }

    }
}