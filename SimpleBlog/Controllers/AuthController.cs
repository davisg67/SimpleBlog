using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        
        public ActionResult Login()
        {
            return View();


            //SEND DATA TO THE VIEW
            //return View(new AuthLogin
            //{
            //    Test = "Test Data to view."  //Test property is defined in ViewModel AuthLogin.
            //});
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
                return View(form);

            //Validating input and custom error msg.
            if (form.Username != "Rainbow Dash")
            {
                ModelState.AddModelError("Username", "Username or password is uncool!");
                return View(form);
            }

            return Content("The form is valid!");
        }
    }
}