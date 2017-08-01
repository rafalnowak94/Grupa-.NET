using ShopASP.ViewModels;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Hosting;
using Microsoft.Owin.Security;
using ShopASP.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ShopASP.App_Start;

namespace ShopASP.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        private ApplicationSignInManager _signInManager;
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

        public IAuthenticationManager AuthenticatManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }

        }
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUtl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserData = new UserData() };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            
            return View(model);

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult LogOff()
        {
            AuthenticatManager.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}