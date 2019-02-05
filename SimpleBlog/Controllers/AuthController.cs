using SimpleBlog.Models;
using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        //Get requests
        public ActionResult Login()
        {
            return View();
        }

        //Post requests
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);

            //Defeat timing test to determine if user is valid. See method for further explanation.
            if (user == null)
                SimpleBlog.Models.User.FakeHash();

            //Validate user name and password.
            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect.");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            //Future upgrade consideration - check that redirection is to proper domain.
            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            return RedirectToRoute("home");
        }
    }
}