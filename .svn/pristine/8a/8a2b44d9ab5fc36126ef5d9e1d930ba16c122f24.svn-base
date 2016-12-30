using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Areas.User.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    [CustomActionFilter]
    public class OrderController : Controller
    {
        // GET: User/Order
        OrderService Context = new OrderService();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OrderDetails()
        {
            List<ViewModelOrder> tblorde = Context.GettblShipmentReturnDetail();
            return View(tblorde.ToList());
        }
    }
}