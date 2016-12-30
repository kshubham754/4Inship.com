using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4InShip.com.Repository;
using _4InShip.com.Models;
using Microsoft.Practices.Unity;
using _4InShip.com.IServices;

namespace _4InShip.com.Services
{

    public class PlanServices : IPlanServices<PlansOptionModel>
    {
        [Dependency]
        public Context4InshipEntities Context { get; set; }

        public List<tblPlan> Plans()
        {
            try
            {
                var PlanList = Context.tblPlans.OrderBy(x => x.price).ToList();
                return PlanList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
        public List<PlansOptionModel> OptionPlan()
        {
            try
            {
                var OptionPlan = (from p in Context.tblPlans join pkoptionlink in Context.tblPlanOptionLinkings on p.Id equals pkoptionlink.Fk_plan_id join pkoption in Context.tblPackagingOptions on pkoptionlink.Fk_packaging_option_id equals pkoption.Id select new { pkoptionlink.price, pkoption.title, pkoption.description, p.Id, pkoption.is_signup ,pkoption.is_shipment}).Where(x => x.is_signup == true).ToList();
                List<PlansOptionModel> objPlansOptionModel = new List<PlansOptionModel>();
                foreach (var item in OptionPlan.ToList())
                {
                    objPlansOptionModel.Add(new PlansOptionModel { title = item.title, price = Convert.ToDecimal(item.price), description = item.description, p_id = item.Id,IsSignUp=Convert.ToBoolean(item.is_signup) });
                }
                return objPlansOptionModel.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}