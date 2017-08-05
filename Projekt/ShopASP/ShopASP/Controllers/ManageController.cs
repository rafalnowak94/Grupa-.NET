using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopASP.App_Start;
using ShopASP.Models;
using ShopASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ShopASP.DAL;
using System.Data.Entity;

namespace ShopASP.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ShopContext db = new ShopContext();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            UpdateSuccess,
            Error
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            if (TempData["ErrorLog"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ErrorLog"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
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
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                //Post-Redirect-Get 
                TempData["ErrorLog"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", new { Message = ManageMessageId.UpdateSuccess });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            // In case we have simple errors - return
            if (!ModelState.IsValid)
            {
                TempData["ErrorLog"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            // In case we have login errors
            if (!ModelState.IsValid)
            {
                TempData["ErrorLog"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }


        public ActionResult OrdersList()

        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;
            IEnumerable<Order> userOrders;

            if (isAdmin)
            {
                userOrders = db.Orders.Include("OrderItems").OrderByDescending(o => o.CreateDate).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOrders = db.Orders.Where(o => o.UserId.ToString() == userId.ToString()).Include("OrderItems").OrderByDescending(o => o.CreateDate).ToArray();

            }

            return View(userOrders);
        }

        public ActionResult EditOrder(int orderId)
        {
            Order order = db.Orders.Where(o => o.OrderId == orderId).Single();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(Order order)
        {
            var orderToUpdate = db.Orders.Find(order.OrderId);
            orderToUpdate.OrderStatus = order.OrderStatus;
            db.SaveChanges();
            return RedirectToAction("OrdersList");
        }

    }


}