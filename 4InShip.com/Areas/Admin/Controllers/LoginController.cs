using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Repository;
using System.Web.Security;
using System.Security.Principal;
using static _4InShip.com.FilterConfig;

namespace _4InShip.com.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        Context4InshipEntities Context = new Context4InshipEntities();
        public ActionResult Index()
        {
            if (Request.QueryString["returnUrl"] != null)
            {
                TempData["retUrl"] = Request.QueryString["returnUrl"].ToString();

            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(tblAdminUser objAdminUser)
        {
            var AdminUser = Context.tblAdminUsers.Where(x => x.username == objAdminUser.username && x.password == objAdminUser.password).Select(x => x).FirstOrDefault();
            if (AdminUser != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                if (cookie == null)
                {
                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

                    HttpContext.Response.Cookies.Add(cookie);
                }
                string UserData = AdminUser.role + ";" + AdminUser.Id + ";" + AdminUser.username;
                string UserName = AdminUser.username;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddDays(1), true, UserData, FormsAuthentication.FormsCookiePath);
                cookie.Value = FormsAuthentication.Encrypt(ticket);
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Set(cookie);
                HttpContext.User = new GenericPrincipal(HttpContext.User.Identity, UserData.Split(';'));
                if (TempData["retUrl"] != null)
                {
                    Response.Redirect(TempData["retUrl"].ToString());

                }
                return RedirectToAction("Index", "PackagingOptions");
            }
            else

            {
                ViewBag.Message = "showMessage('User does not exist!',false);";
            }
            return View();
        }

        public void SignOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Admin/Login", true);
        }
    }
}