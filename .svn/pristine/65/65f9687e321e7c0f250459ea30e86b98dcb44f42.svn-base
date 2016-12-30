using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static _4InShip.com.FilterConfig;

namespace _4InShip.com.Areas.Admin.Controllers
{
    [CustomAuthorization(Url = "~/Admin/Login")]
    [Authorize(Roles = "Admin")]
    [IsAuthenticateAdminFilter]
    public class PackagingOptionsController : Controller
    {
        // GET: Admin/PackagingOptions
        PackagingOptionsService Context = new PackagingOptionsService();

       
        public ActionResult Index(int? Id)
        {
            if (Id != null)
            {

                return View(Context.GetblPackagingOptionsById(Id ?? 0).SingleOrDefault());
            }
            return View();
        }
        public ActionResult InsertTblPackagingOptions()
        {
            return View("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertTblPackagingOptions(tblPackagingOption tblpackagingoption)
        {
            try
            {
                if (tblpackagingoption != null)
                {
                    ViewBag.Message = Context.InsertUpdatePackagingOptions(tblpackagingoption);
                   
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

            }
            tblPackagingOption roll = new tblPackagingOption();
            ModelState.Clear();
            return View("Index");
        }
        public ActionResult UpdateTblPackagingOptions(tblPackagingOption tblpackagingoption)
        {

            try
            {
                if (tblpackagingoption != null)
                {
                    ViewBag.Message = Context.InsertUpdatePackagingOptions(tblpackagingoption);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

            }
            tblPackagingOption roll = new tblPackagingOption();
            ModelState.Clear();

            return View("Index");
        }
        [HttpGet]
        public ActionResult PackagingOptions()
        {
            tblPackagingOption roll = new tblPackagingOption();

            List<tblPackagingOption> lst = Context.GettblPackagingOptions().ToList();

            return View("GetPackagingOptionDetail", lst);
        }
        public JsonResult ChangeStatusPackagingOptionById(int Id)
        {
            if (Id != 0)
            {
               ViewBag.Message= Context.ChangeStatustblPackagingOptions(Id);
            }
            return Json(Id.ToString(), JsonRequestBehavior.AllowGet);

        }
    }
}