using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using static _4InShip.com.FilterConfig;
namespace _4InShip.com.Areas.Admin.Controllers
{
    [CustomAuthorization(Url = "~/Admin/Login")]
    [Authorize(Roles = "Admin")]
    [IsAuthenticateAdminFilter]
    public class ReturnPackagesController : Controller
    {
        // GET: Admin/ReturnPackages
        ReturnPackagesService Context = new ReturnPackagesService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnPackages()
        {
            List<View_ModelReturnPackage> lst = Context.GettblShipmentReturnDetail().ToList();
            return View("ReturnPackages", lst);
        }
    }
}