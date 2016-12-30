using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{
    public class PlanServices : clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();

        public string InsertUpdatePlan(tblPlan objtblPlan)
        {
            try
            {
                if (objtblPlan.Id != 0)
                {
                    tblPlan tbl = new tblPlan();
                    tbl.Id = objtblPlan.Id;
                    tbl.title = objtblPlan.title;
                    tbl.description = objtblPlan.description;
                    tbl.price = objtblPlan.price;
                    tbl.free_storage_days = objtblPlan.free_storage_days;
                    tbl.is_recurring = objtblPlan.is_recurring;
                    tbl.recurring_duration = 1;
                    tbl.modified_on = DateTime.Now;
                    tbl.status = objtblPlan.status;
                    tbl.modified_on = DateTime.Now; ;
                    Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                    return SuccessMsg("Updated successfully");
                }
                else
                {
                    objtblPlan.recurring_duration = 1;
                    objtblPlan.created_on = DateTime.Now;
                    Context.tblPlans.Add(objtblPlan);
                }
                Context.SaveChanges();
                return SuccessMsg("Saved successfully");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<tblPlan> GettblPlans()
        {
            return Context.tblPlans.AsQueryable().OrderByDescending(x => x.created_on).ToList();
        }
        public string ChangeStatusTblPlanById(int Id)
        {
            try
            {
                var ItemToRemove = Context.tblPlans.AsQueryable().SingleOrDefault(x => x.Id == Id);
                if (ItemToRemove != null)
                {
                    ItemToRemove.status = !ItemToRemove.status;
                    Context.Entry(ItemToRemove).State = System.Data.Entity.EntityState.Modified;
                }
                Context.SaveChanges();
                return SuccessMsg("Status has been changed successfully");
            }
            catch (Exception ex)

            {
                return SuccessMsg(ex.Message);
            }
        }
        public string ChangeIS_RecurringTblPlanById(int Id)
        {
            try
            {
                var ItemToRemove = Context.tblPlans.AsQueryable().SingleOrDefault(x => x.Id == Id);
                if (ItemToRemove != null)
                {
                    ItemToRemove.is_recurring = !ItemToRemove.is_recurring;
                    Context.Entry(ItemToRemove).State = System.Data.Entity.EntityState.Modified;

                }
                Context.SaveChanges();
                return SuccessMsg("Status has been changed successfully");
            }
            catch (Exception ex)

            {
                return SuccessMsg(ex.Message);
            }

        }
        public List<tblPlan> GetblPlansById(int id)
        {
            return Context.tblPlans.AsQueryable().Where(x => x.Id == id).OrderByDescending(x => x.created_on).ToList();
        }
    }
}