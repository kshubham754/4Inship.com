using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _4InShip.com.Areas.Admin.Models
{
    public class clsAuthenticateAdmin
    {
        public bool IsUserAuthenticated { get; set; } = false;


        public void CheckUserAuthenticated()
        {
            if (IsUserAuthenticated == false)

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    if (HttpContext.Current.User.IsInRole("Admin"))
                    {
                        HttpSessionStateBase Session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                        FormsIdentity fi;
                        fi = (FormsIdentity)HttpContext.Current.User.Identity;
                        string[] ud = fi.Ticket.UserData.Split(';');
                        Session["AdminId"] = Convert.ToInt32((string.IsNullOrEmpty(ud[1]) ? "0" : ud[1]));
                        Session["AdminUsername"] = ud[2];
                        IsUserAuthenticated = true;
                    }
                    else
                    {
                        IsUserAuthenticated = false;
                        HttpContext.Current.Session.Abandon();
                        FormsAuthentication.SignOut();
                        HttpContext.Current.Response.Redirect("~/Admin/Login");
                    }
                }
                else
                {
                    IsUserAuthenticated = false;
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Response.Redirect("~/Admin/Login");
                }
        }
    }
}