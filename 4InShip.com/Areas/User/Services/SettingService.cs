using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using _4InShip.com.Areas.User.IServices;
using _4InShip.com.Areas.User.Models;
using _4InShip.com.Services;
using System.Web.Mvc;
using System.Net.Mail;

namespace _4InShip.com.Areas.User.Services
{
    public class SettingService : Admin.Models.clsCommon, ISettingService<tblAddressBook, tblCustomer>
    {
        [Dependency]
        public Context4InshipEntities Context { get; set; }
        public string CreateUpdateNewAddress(tblAddressBook tbladdressbook)
        {
            try
            {
                if (tbladdressbook.Id != 0)
                {
                    var tbl = Context.tblAddressBooks.AsQueryable().Where(x => x.Id == tbladdressbook.Id).FirstOrDefault();
                    tbl.Id = tbladdressbook.Id;
                    tbl.first_name = tbladdressbook.first_name;
                    tbl.last_name = tbladdressbook.last_name;
                    tbl.address1 = tbladdressbook.address1;
                    tbl.address2 = tbladdressbook.address2;
                    tbl.address_name = tbladdressbook.address_name;
                    tbl.company = tbladdressbook.company;
                    tbl.country_code = tbladdressbook.country_code;
                    tbl.city = tbladdressbook.city;
                    tbl.state = tbladdressbook.state;
                    tbl.mobile1 = tbladdressbook.mobile1;
                    tbl.mobile2 = tbladdressbook.mobile2;
                    tbl.post_code = tbladdressbook.post_code;
                    tbl.tax_id = tbladdressbook.tax_id;

                    Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                    Context.SaveChanges();
                    return SuccessMsg("Address book has been updated successfully");
                }
                else
                {
                    tbladdressbook.created_on = DateTime.Now;

                    Context.tblAddressBooks.Add(tbladdressbook);
                    Context.SaveChanges();
                    return SuccessMsg("Address book has been created successfully");
                }
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
        }
        public List<tblAddressBook> GetAddressBookData(int? Id, bool Is_Default)
        {
            try
            {
                int id = (int)HttpContext.Current.Session["UserId"];
                var err = Context.tblAddressBooks.AsQueryable().Where(x => x.Fk_customer_Id == id && x.is_default == Is_Default).ToList();
                return Context.tblAddressBooks.AsQueryable().Where(x => x.Fk_customer_Id == id && x.is_default == Is_Default).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public tblAddressBook GetAddressBookData(int id)
        {
            try
            {
                var err = Context.tblAddressBooks.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
                return Context.tblAddressBooks.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public tblCustomer GetTblCustomerData()
        {
            try
            {
                int id = (int)HttpContext.Current.Session["UserId"];
                return Context.tblCustomers.AsQueryable().Where(x => x.Id == id).OrderByDescending(x => x.created_on).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteSingleAddressByID(int id)
        {
            var itemToRemove = Context.tblAddressBooks.SingleOrDefault(x => x.Id == id);

            try
            {
                if (itemToRemove != null)
                {
                    Context.tblAddressBooks.Remove(itemToRemove);
                    Context.SaveChanges();

                }
                return SuccessMsg("Your address has been deleted successfully");
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
        }
        public string ChangeUserPassword(ChangePasswordModel changepasswordmodel)
        {
            try
            {
                tblCustomer cust = new Repository.tblCustomer();
                var tbl = Context.tblCustomers.AsQueryable().Where(x => x.Id == Convert.ToInt32(HttpContext.Current.Session["UserId"])).FirstOrDefault();
                if (changepasswordmodel.confirmpassword == changepasswordmodel.newpassword)
                {
                    tbl.password = changepasswordmodel.confirmpassword;
                    Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                    Context.SaveChanges();
                }
                return SuccessMsg("Password has been changed successfully");


            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }

        }
        public bool CheckDefaultAddress(string address, string adressBookId)
        {
            try
            {
                if (Context == null)
                    Context = new Context4InshipEntities();
                int id = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                int adressBook_Id = Convert.ToInt32(((adressBookId == "" || adressBookId == "SingleAddress" || adressBookId == "Setting") ? "0" : adressBookId));
                List<tblAddressBook> e = Context.tblAddressBooks.AsQueryable().Where(x => x.address_name == address && x.Fk_customer_Id == id && x.Id != (adressBook_Id == 0 ? 0 : adressBook_Id)).ToList();
                if (e.Any())
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool CheckPassword(string pass)
        {
            try
            {
                int id = (int)HttpContext.Current.Session["UserId"];
                var tbl = Context.tblCustomers.AsQueryable().Where(x => x.password == pass && x.Id == id).FirstOrDefault();
                if (tbl != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public string ChangeUserEmail(ChangeEmailId changeemailId)
        {
            try
            {
                tblCustomer cust = new Repository.tblCustomer();
                var tbl = Context.tblCustomers.AsQueryable().Where(x => x.Id == Convert.ToInt32(HttpContext.Current.Session["UserId"])).FirstOrDefault();
                if (changeemailId.confirmemail == changeemailId.newemail)
                {
                    tbl.email = changeemailId.confirmemail;
                    Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;

                }
                Context.SaveChanges();
                return SuccessMsg("Email id has been changed successfully");
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }

        }
        public string CreateUpdateBillingAddress(tblBillingAddress tblbillingaddress)
        {
            try
            {
                if (tblbillingaddress.Id != 0)
                {
                    var tbl = Context.tblBillingAddresses.Where(x => x.Id == tblbillingaddress.Id).FirstOrDefault();
                    tbl.Id = tblbillingaddress.Id;
                    tbl.first_name = tblbillingaddress.first_name;
                    tbl.last_name = tblbillingaddress.last_name;
                    tbl.email = tblbillingaddress.email;
                    tbl.mobile = tblbillingaddress.mobile;
                    tbl.address = tblbillingaddress.address;
                    tbl.country_code = tblbillingaddress.country_code;
                    tbl.state = tblbillingaddress.state;
                    tbl.city = tblbillingaddress.city;
                    tbl.post_code = tblbillingaddress.post_code;
                    Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                    Context.SaveChanges();
                    return SuccessMsg("Updated successfully");
                }
                else
                {
                    tblbillingaddress.created_on = DateTime.Now;
                    Context.tblBillingAddresses.Add(tblbillingaddress);
                    Context.SaveChanges();
                    return SuccessMsg("Saved successfully");
                }
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
        }
        public string insertrewardpoint(int balance)
        {
            try
            {
                if (balance != 0)
                {
                    tblRewardAmount objreward = new tblRewardAmount();
                    tblRewardPoint objrewardpoint = new tblRewardPoint();
                    int Id = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                    objreward.Fk_CustomerId = Id;
                    objreward.credit = Convert.ToDecimal(balance);
                    objreward.debit = Convert.ToDecimal(0);
                    objreward.created_on = DateTime.Now;
                    Context.tblRewardAmounts.Add(objreward);
                    List<tblRewardPoint> rewardpoint = Context.tblRewardPoints.AsQueryable().Where(x => x.Fk_CustomerId == Id).ToList();
                    if (rewardpoint != null)
                    {
                        foreach (var item in rewardpoint)
                        {
                            item.is_converted = true;
                            Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                        Context.SaveChanges();
                    }

                }
                Context.SaveChanges();
                return SuccessMsg("Your points converted successfully");
            }
            catch (Exception ex)
            {


                return ErrorMsg(ex.Message);
            }


        }
    }
}