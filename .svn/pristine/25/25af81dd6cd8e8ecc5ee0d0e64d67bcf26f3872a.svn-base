using System;
using _4InShip.com.Repository;
using System.Linq;
using System.Collections.Generic;
using _4InShip.com.Areas.Admin.Models;

namespace _4InShip.com.Areas.Admin.Services
{
    public class PackagingOptionsService: clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();

        public string InsertUpdatePackagingOptions(tblPackagingOption tblpackagingoption)
        {
            try
            {
                if (tblpackagingoption.Id != 0)
                {
                    var Ids = tblpackagingoption.Id;
                    tblPackagingOption tbl = Context.tblPackagingOptions.AsQueryable().Where(x => x.Id == Ids).FirstOrDefault();
                    if (tbl != null)
                    {
                        tbl.Id = tblpackagingoption.Id;
                        tbl.title = tblpackagingoption.title;
                        tbl.description = tblpackagingoption.description;
                        tbl.status = true;
                        tbl.modified_on = DateTime.Now;
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                        Context.SaveChanges();
                        return SuccessMsg("Updated successfully");
                    }
                }
                else
                {
                    tblpackagingoption.created_on = DateTime.Now;
                    tblpackagingoption.status = true;
                    Context.tblPackagingOptions.Add(tblpackagingoption);
                    //  Context.Configuration.ValidateOnSaveEnabled = false;                 
                }
                Context.SaveChanges();
                return SuccessMsg("Saved successfully");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public List<tblPackagingOption> GetblPackagingOptionsById(int Id)
        {

            var lst = (from list in Context.tblPackagingOptions.Where(x => x.Id == Id) select list).ToList();
            return lst.ToList();
        }
        public List<tblPackagingOption> GettblPackagingOptions()
        {

            return Context.tblPackagingOptions.OrderByDescending(x=>x.created_on).ToList();
        }
        public string ChangeStatustblPackagingOptions(int Id)
        {
            try
            {
                var itemToRemove = Context.tblPackagingOptions.SingleOrDefault(x => x.Id == Id);
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


    }
}