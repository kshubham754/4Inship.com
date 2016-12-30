using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Areas.User.Models;
using System.Web.Security;

namespace _4InShip.com.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    [CustomActionFilter]
    public class AccountController : Controller
    {
        // GET: User/Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]   
        public void SignOut()
        {
            (new clsAuthenticateData()).IsUserAuthenticated = false;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}