using System;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Areas.Admin.Models;

namespace _4InShip.com
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        public class CustomAuthorization : AuthorizeAttribute
        {
            public string Url { get; set; }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectResult(Url + "?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery, true);
            }
        }
    }

    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            (new _4InShip.com.Areas.User.Models.clsAuthenticateData()).CheckUserAuthenticated();
        }
    }
    public class IsAuthenticateAdminFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            (new _4InShip.com.Areas.Admin.Models.clsAuthenticateAdmin()).CheckUserAuthenticated();
        }
    }

}
