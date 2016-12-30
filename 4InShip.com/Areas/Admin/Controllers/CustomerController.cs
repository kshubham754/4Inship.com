using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        Customerservice Context = new Customerservice();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Customers()
        {
            List<tblCustomer> custList = Context.GetCustomerDetails();
            return View("Customers", custList);
        }
        public JsonResult ChangeStatusCustomerById(int id)
        {
            if (id != 0)
            {
                ViewBag.Message = Context.ChangeStatustblCustomer(id);
            }
            return Json(id.ToString(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ChangepaymentStatusCustomerById(int Id)
        {
            if (Id != 0)
            {
                ViewBag.Message = Context.ChangePaymentStatustblCustomer(Id);
            }
            return Json(Id.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}