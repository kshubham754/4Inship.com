using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{
    public class AdminUsersService:clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public string InsertUpdateAdminUsers(tblAdminUser tbladminuser)
        {
            try
            {
                if (tbladminuser.Id != 0)
                {
                    var Ids = tbladminuser.Id;
                    var tbl = Context.tblAdminUsers.Where(x => x.Id == Ids).FirstOrDefault();
                    if (tbl != null)
                    {
                        tbl.Id = tbladminuser.Id;
                        tbl.name = tbladminuser.name;
                        tbl.username = tbladminuser.username;
                        tbl.password = tbladminuser.password;
                        tbl.role = tbladminuser.role;
                        tbl.status = true;
                        tbl.modified_on = DateTime.Now;
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                        return SuccessMsg("Updated successfully");
                    }
                }
                else
                {
                    tbladminuser.created_on = DateTime.Now;
                    tbladminuser.status = true;
                    Context.tblAdminUsers.Add(tbladminuser);
                }
                Context.SaveChanges();
                return SuccessMsg("Saved successfully");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public List<tblAdminUser> GettbltblAdminUserById(int Id)
        {

            var lst = (from list in Context.tblAdminUsers.Where(x => x.Id == Id) select list).ToList();
            return lst.ToList();
        }
        public List<tblAdminUser> GettbltblAdminUser()
        {

            return Context.tblAdminUsers.ToList();
        }
        public void DeletetblAdminUser(int Id)
        {
            var itemToRemove = Context.tblAdminUsers.OrderByDescending(x => x.created_on).SingleOrDefault(x => x.Id == Id);
            if (itemToRemove != null)
            {
                Context.tblAdminUsers.Remove(itemToRemove);
                Context.SaveChanges();
            }
        }
        public string ChangeStatusTblUserById(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var RemoveToItem = Context.tblAdminUsers.SingleOrDefault(x => x.Id == Id);
                    if (RemoveToItem != null)
                    {
                        RemoveToItem.status = !RemoveToItem.status;
                        Context.Entry(RemoveToItem).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                Context.SaveChanges();
                return SuccessMsg("Status has been changed successfully");
            }
            catch(Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
            

        }
    }
}