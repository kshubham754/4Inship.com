﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Models;
using _4InShip.com.Services;
using _4InShip.com.IServices;
using _4InShip.com.Repository;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Routing;
using static _4InShip.com.FilterConfig;

namespace _4InShip.com.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        private ICustomerServices<ViewCustomerModel, ViewCustomerPaymentModel> _reposatory;
        CountryServices objCountryServices = new CountryServices();

        public CustomerController(ICustomerServices<ViewCustomerModel, ViewCustomerPaymentModel> repo)
        {
            _reposatory = repo;
        }
        public ActionResult Index()
        {

            ViewBag.CountryBind = objCountryServices.Country();
            return View();
        }
        public ActionResult SignUp(string id)
        {
            if (id != null)
            {
                string Cust_id = Cryptography.Decrypt(id);
                TempData["cust_id"] = Cust_id;
                TempData.Peek("cust_id");
                ViewBag.Country = objCountryServices.Country();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Country = objCountryServices.Country();
            return View();
        }

        //--Customer SignUp --//
        [HttpPost]
        public ActionResult SignUp(ViewCustomerModel objViewCustomerModel)
        {
            objViewCustomerModel.Plan_id = Convert.ToInt32(TempData.Peek("cust_id"));
            ViewBag.Message = _reposatory.CustomerSignUp(objViewCustomerModel);
            if (ViewBag.Message == "showMessage('User already exist!',false);")
            {
                ViewBag.Country = objCountryServices.Country();
                return View();
            }
            else if (ViewBag.Message == "Register successfully redirect login page")
            {
                TempData["SignUpMessage"] = "showMessage('You has been register successfully !',true);";
                return RedirectToAction("Login", "Customer");
            }
            ModelState.Clear();

            string Q = ViewBag.Message + "&Type=SignUp";
            ViewBag.Country = objCountryServices.Country();
            return RedirectToAction("CustomerPayment", "Customer", new { Q });
        }
        public JsonResult CheckCoupon(ViewCustomerModel objViewCustomerModel)
        {
            string data = "";
            if (Request.Form["CouponCode"] != null)
            {
                objViewCustomerModel.CuponCode = Request.Form["CouponCode"];

                string SignUpCouponCode = ConfigurationManager.AppSettings["SignUpCouponCode"].ToString();
                decimal SignUpCouponValue = Convert.ToDecimal(ConfigurationManager.AppSettings["SignUpCouponValue"]);

                if (objViewCustomerModel.CuponCode.ToLower().Trim() == SignUpCouponCode.ToLower().Trim())
                {

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data = "Coupon does not exist!";
                }
            }
            else
            {
                data = "Coupon does not exist!";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerPayment()
        {
            ViewBag.Country = objCountryServices.Country();
            return View();
        }
        //-- Get Address Customer show Billing Page and Check AccountExpireDate Method --//
        public JsonResult GetAddressbookData()
        {
            string CustomerID;
            if (Session["customerDycriptID"] != null)
            {
                CustomerID = Session["customerDycriptID"].ToString();
                int id = Convert.ToInt32(Cryptography.Decrypt(CustomerID));
                var GetAddressBook = _reposatory.GetAddressBookData(id);
                return Json(GetAddressBook, JsonRequestBehavior.AllowGet);
            }
            else if (Session["CustomerExpDateAccount"] != null)
            {
                CustomerID = Session["CustomerExpDateAccount"].ToString();
                int id = Convert.ToInt32(Cryptography.Decrypt(CustomerID));
                var GetAddressBook = _reposatory.GetAddressBookData(id);
                return Json(GetAddressBook, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpGet]
        public ActionResult Login(string login)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("User"))
                {
                    FormsIdentity fi;
                    fi = (FormsIdentity)User.Identity;
                    string[] ud = fi.Ticket.UserData.Split(';');
                    Session["UserId"] = Convert.ToInt32((string.IsNullOrEmpty(ud[1]) ? "0" : ud[1]));
                    Session["UserFullName"] = ud[2];
                    Session["UserEmailAddress"] = (ud.Length >= 3 ? "" : ud[3]);
                    return RedirectToAction("Index", new RouteValueDictionary(new { controller = "User/Home", action = "Index" }));
                }
            }
            ViewCustomerModel tbl = new ViewCustomerModel();
            if (Request.Cookies["UserPassword"] != null)
            {

                string e = Cryptography.Decrypt(Request.Cookies["UserPassword"].Values["UserName"]);
                string p = Cryptography.Decrypt(Request.Cookies["UserPassword"].Values["Password"]);
                tbl.email = e;
                tbl.password = p;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CustomerPaymentBillingAddress(ViewCustomerPaymentModel objViewCustomerPaymentModel)
        {
            string CustomerID = "";
            if (Session["CustomerExpDateAccount"] != null)
            {
                CustomerID = Session["CustomerExpDateAccount"].ToString();
            }
            else if (Session["customerDycriptID"] != null)
            {
                CustomerID = Session["customerDycriptID"].ToString();
            }
            int id = Convert.ToInt32(Cryptography.Decrypt(CustomerID));
            objViewCustomerPaymentModel.Fk_customer_id = id;
            TempData["CustomerBillingPayment"] = _reposatory.CustomerPayBillingAddress(objViewCustomerPaymentModel);
            Session["CustomerExpDateAccount"] = null;
            Session["customerDycriptID"] = null;
            return Redirect("CustomerPayment");
        }
        //--- Customer Login Method--//
        [HttpPost]
        public ActionResult Login(ViewCustomerModel tblcustomer, string IsRemember)
        {
            if (tblcustomer != null)
            {


                var user = _reposatory.GetTblCustomerIdByUserNamePassword(tblcustomer);
                if (user != null)
                {
                    Session["LoginUserId"] = user.Id;
                    if (user.AccountExpDate == "")
                    {
                        string EncriptID = Cryptography.Encrypt(Convert.ToString(user.Id));
                        Session["CustomerExpDateAccount"] = EncriptID;
                        string Q = EncriptID + "&Type=ExpireDate";
                        TempData["Message"] = "showMessage('Complete your payment in order to login your account!',false);";
                        return RedirectToAction("CustomerPayment", "Customer", new { Q });
                    }
                    else if (user != null && user.AccountExpDate != "")
                    {

                        if (IsRemember == "true")
                        {
                            HttpContext.Request.Cookies.Remove("UserPassword");
                            HttpCookie Cookie = new HttpCookie("UserPassword");
                            string em = Cryptography.Encrypt(tblcustomer.email);
                            Cookie.Values.Add("UserName", em);
                            string ps = Cryptography.Encrypt(tblcustomer.password);
                            Cookie.Values.Add("Password", ps);
                            Cookie.Expires = DateTime.Now.AddDays(100);
                            Response.Cookies.Add(Cookie);
                        }
                        HttpCookie cookie = HttpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                        if (cookie == null)
                        {
                            cookie = new HttpCookie(FormsAuthentication.FormsCookieName);

                            HttpContext.Response.Cookies.Add(cookie);
                        }
                        string UserData = "User" + ";" + user.Id + ";" + user.FirstName + user.LastName + ";" + user.email;
                        string UserName = user.email;
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddDays(1), true, UserData, FormsAuthentication.FormsCookiePath);
                        cookie.Value = FormsAuthentication.Encrypt(ticket);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        HttpContext.Response.Cookies.Set(cookie);
                        HttpContext.User = new GenericPrincipal(HttpContext.User.Identity, UserData.Split(';'));
                        return RedirectToAction("Index", new RouteValueDictionary(new { controller = "User/Home", action = "Index" }));
                    }
                }
                else
                {
                    ViewBag.Message = "showMessage('User does not exist!',false);";
                }
            }
            return View();
        }
        public ActionResult ForgetPassword()
        {


            return View();
        }
        [HttpPost]

        public ActionResult ForgetPassword([Bind(Include = "email")]ViewCustomerModel emambmbnil)
        {

            string emails = emambmbnil.email;
            var user = _reposatory.GetTblCustomerIdByUserNamePasswords(emails);
            if (user != null)
            {
                string pass = _reposatory.UpdateUserEmailPassword(emails);
                ClsCommanCustomerSignup clscommn = new ClsCommanCustomerSignup();
                string subject = "Password has been changed";
                string body = "Your Password Is:" + pass;
                Dictionary<string, string> dicTemplate = new Dictionary<string, string>();
                dicTemplate.Add("#header#", "4Inship - Change password");
                dicTemplate.Add("#content#", body);
                string mailbody = clscommn.Emailtemplate(dicTemplate);
                try
                {
                    clscommn.SendEmail(emails, "", body, subject);
                    ViewBag.Message = "showMessage('Your Password has been send to your Registered Email Address'); ";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "showMessage('User does not exist!',false);";
            }
            return View();
        }

    }


}