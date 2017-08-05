using Microsoft.AspNet.Identity.Owin;
using ShopASP.App_Start;
using ShopASP.DAL;
using ShopASP.Infastructure;
using ShopASP.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ShopASP.Models;

namespace ShopASP.Controllers
{
    public class BasketController : Controller
    {
        private CartManager cartManager;

        private ISessionFuncs sessionManager;

         private ShopContext db;

        private ApplicationUserManager _userManager;

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

        public ApplicationUserManager UserManager
        {

            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                _userManager = value;
            }

        }
        public async Task<ActionResult> Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Address = user.UserData.Address,
                    CodeAndCity = user.UserData.CodeAndCity,
                    Email = user.Email,
                    PhoneNumber = user.UserData.PhoneNumber
                };

                return View(order);
            }

            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Basket") });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Checkout(Order Orderdetalis)
        {

            if (ModelState.IsValid)
            {
                // Get user
                var userId = User.Identity.GetUserId();

                // Save Order
                CartManager cartManager = new CartManager(this.sessionManager, this.db);
                var newOrder = cartManager.CreateOrder(Orderdetalis, userId);

                // Update profile information
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                // Empty cart
                cartManager.EmptyCart();

                return RedirectToAction("OrderSuccess");

            }
            else
            {
                return View(Orderdetalis);
            }

        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}