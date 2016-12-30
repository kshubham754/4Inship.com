using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Services;
using _4InShip.com.Models;
using _4InShip.com.IServices;
using System.Configuration;

namespace _4InShip.com.Controllers
{
    public class HomeController : Controller
    {
        private IPlanServices<PlansOptionModel> _reposatory;
        public HomeController(IPlanServices<PlansOptionModel> repo)
        {
            _reposatory = repo;
        }
        public ActionResult Index()
        {
            
            (new Services.ClsCommanCustomerSignup()).GetCustomerReference();
            PlanServices Obj = new PlanServices();
           
            ViewBag.PlansOption = _reposatory.Plans();
            List<PlansOptionModel>objList= _reposatory.OptionPlan();
            return View(objList);
        }
        


    }
}