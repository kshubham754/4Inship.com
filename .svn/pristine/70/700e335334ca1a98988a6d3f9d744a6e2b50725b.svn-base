using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _4InShip.com.Areas.User.Models
{
    public class clsAuthenticateData
    {

        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserEmailAddress { get; set; }
        public bool IsUserAuthenticated { get; set; } = false;


        public void CheckUserAuthenticated()
        {
            if (IsUserAuthenticated == false)

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.IsInRole("User"))
                    {
                        HttpSessionStateBase Session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                        FormsIdentity fi;
                        fi = (FormsIdentity)HttpContext.Current.User.Identity;
                        string[] ud = fi.Ticket.UserData.Split(';');
                        Session["UserId"]= Convert.ToInt32((string.IsNullOrEmpty(ud[1]) ? "0" : ud[1]));
                        Session["UserFullName"] = ud[2];
                        Session["UserEmailAddress"] = (ud.Length >= 3 ? "" : ud[3]);
                        IsUserAuthenticated = true;
                    }
                    else
                    {
                        IsUserAuthenticated = false;
                        HttpContext.Current.Session.Abandon();
                        FormsAuthentication.SignOut();
                        FormsAuthentication.RedirectToLoginPage();
                    }
                }
                else
                {
                    IsUserAuthenticated = false;
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
        }
    }
}