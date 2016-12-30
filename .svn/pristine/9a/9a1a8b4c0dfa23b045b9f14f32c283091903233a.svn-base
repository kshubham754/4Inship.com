using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static _4InShip.com.FilterConfig;

namespace _4InShip.com.Areas.Admin.Controllers
{
    [CustomAuthorization(Url = "~/Admin/Login")]
    [Authorize(Roles = "Admin")]
    [IsAuthenticateAdminFilter]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        OrderService Context = new OrderService();
        public ActionResult Index(int? Id)
        {
            if (Id != null)
            {
                return View(Context.GetTblOrderById(Id ?? 0).SingleOrDefault());
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertTblOrde(tblOrder Order)
        {
            try
            {
                if (Order != null)
                {
                    ViewBag.Message = Context.InsertUpdateOrder(Order);
                    var err = ViewBag.Message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            tblOrder roll = new tblOrder();
            ModelState.Clear();
            return View("Index", roll);
        }
        public ActionResult UpdateTblPackagingOptions(tblOrder Order)
        {
            try
            {
                if (Order != null)
                {
                    Context.InsertUpdateOrder(Order);
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            tblOrder roll = new tblOrder();
            ModelState.Clear();
            return View("Index", roll);
        }
        [HttpGet]
        public ActionResult Orders()
        {
            List<ViewModelOrder> lst = Context.GetOrder().ToList();
            return View("Orders", lst);
        }
        public JsonResult DeleteOrderById(int Id)
        {
            if (Id != 0)
            {
                Context.DeletetblTblOrder(Id);
            }
            return Json(Id.ToString(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Orderbox(int ids)
        {
            TempData["Id"] = ids;
            return View();
        }
        [HttpPost]
        public ActionResult Orderbox(tblOrderBox tblorderbox)
        {
            if (tblorderbox != null)
            {
                tblorderbox.Fk_order_id = Convert.ToInt32(TempData["Id"]);
                ViewBag.Message = Context.CreateOrderBox(tblorderbox);
            }
            return RedirectToAction("Orders");
        }
    }
}