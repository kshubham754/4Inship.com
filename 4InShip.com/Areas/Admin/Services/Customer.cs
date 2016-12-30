using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{

    public class Customerservice : clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public List<tblCustomer> GetCustomerDetails()
        {
            return Context.tblCustomers.ToList();
        }
        public string ChangeStatustblCustomer(int id)
        {
            try
            {
                var itemToRemove = Context.tblCustomers.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
                if (itemToRemove != null)
                {
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    itemToRemove.status = !itemToRemove.status;
                    Context.Entry(itemToRemove).State = System.Data.Entity.EntityState.Modified;
                }
                Context.SaveChanges();
                return SuccessMsg("Status changed successfully");

            }
            catch (Exception ex)
            {

                return ErrorMsg(ex.Message);
            }

        }
        public string ChangePaymentStatustblCustomer(int id)
        {
            try
            {
                tblCustomer itemToremove = Context.tblCustomers.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
                if (itemToremove != null)
                {
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    itemToremove.payment_status = !itemToremove.payment_status;
                    Context.Entry(itemToremove).State = System.Data.Entity.EntityState.Modified;
                }
                Context.SaveChanges();
                return SuccessMsg("Payment status changed successfully");

            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }

        }

    }

}