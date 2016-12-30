using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Repository;
using _4InShip.com.Areas.Admin.Services;
using static _4InShip.com.FilterConfig;

namespace _4InShip.com.Areas.Admin.Controllers
{
    [CustomAuthorization(Url = "~/Admin/Login")]
    [Authorize(Roles = "Admin")]
    [IsAuthenticateAdminFilter]
    public class PlansController : Controller
    {
        // GET: Admin/Home
        PlanServices Context = new PlanServices();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerPlans(int? Id)
        {
            if (Id != null)
                  

            {

                return View(Context.GetblPlansById(Id ?? 0).SingleOrDefault());
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerPlans(tblPlan objtblPlan,string IsRecurring,string Status)
        {
            
            if (objtblPlan!=null)
            {
                if (IsRecurring == "true")
                {
                    objtblPlan.is_recurring =true;
                }
                if (Status == "true")
                {
                    objtblPlan.status = true;
                }
                ViewBag.Message = Context.InsertUpdatePlan(objtblPlan);
            }
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult Plans()
        {
           List<tblPlan> lst= Context.GettblPlans();
            return View("GetPlanDetail", lst);
        }
        public JsonResult ChangeStatusTblPlanById(int id)
        {
            if (id!=0)
            {
               ViewBag.Message= Context.ChangeStatusTblPlanById(id);
            }
            return Json(id, ToString(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult ChangeIS_RecurringTblPlanById(int id)
        {
            if (id != 0)
            {
                ViewBag.Message = Context.ChangeIS_RecurringTblPlanById(id);
            }
            return Json(id, ToString(), JsonRequestBehavior.AllowGet);

        }
    }
}