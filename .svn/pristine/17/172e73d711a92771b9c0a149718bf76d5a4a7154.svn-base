using _4InShip.com.Areas.User.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Repository;
using _4InShip.com.Areas.User.IServices;
using _4InShip.com.Areas.User.Models;
using _4InShip.com.Services;
using _4InShip.com.Areas.Admin.Models;
using System.Configuration;

namespace _4InShip.com.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    [CustomActionFilter]
    public class SettingController : Controller
    {
        // GET: User/Setting


        private ISettingService<tblAddressBook, tblCustomer> _repository;
        private IShipmentService<tblReceivedShipment, tblOrder, UserViewCustomerModel, UserPaymentModel, ViewRecivedShipmentModel, tblAddressBook, ViewPackagingOptionsModel, ViewOrderModel, ShipmentMethodPackage, PacakageOptionArray> _repositoryshipment;
      
        public SettingController(SettingService repo, ShipmentsService repos)
        {
            _repository = repo;
            _repositoryshipment = repos;
        }
        
        public ActionResult Index()
        {
            CountryServices objcon = new CountryServices();
           
            ViewBag.country_code = objcon.Country();
            return View(_repository.GetAddressBookData(null, true).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Index(tblAddressBook tbladdressbook)
        {
            if (tbladdressbook != null)
            {
                ViewBag.Message = _repository.CreateUpdateNewAddress(tbladdressbook);

            }
            CountryServices objcon = new CountryServices();
            ViewBag.country_code = objcon.Country();
            return View(_repository.GetAddressBookData(null, true).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult SingleAddress(int? Id)
        {
            CountryServices objcon = new CountryServices();
            ViewBag.country_code = objcon.Country();
            if (Id != null)
            {
                if (Id != 0)
                {
                  
                    return View(_repository.GetAddressBookData(Id ?? 0));
                    { }
                }

            }
            return View();
        }

        public JsonResult CheckAddressname(string addressname, string addressBook_Id)
        {
            bool isMatch = false;

            if (addressname != null)
            {
                isMatch = _repository.CheckDefaultAddress(addressname.Trim(), addressBook_Id);
            }


            return Json(isMatch, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SingleAddress(tblAddressBook tbladdressbook)
        {
            if (tbladdressbook != null)
            {


                //string MSG=  _repository.CheckDefaultAddress(tbladdressbook.address_name);
                //if (MSG != "")
                //{
                //    ViewBag.Message = MSG;
                //    ModelState.Clear();
                //    CountryServices objcond = new CountryServices();
                //    ViewBag.country_code = objcond.Country();
                //    return View();
                //}
                //else
                //{
                tbladdressbook.Fk_customer_Id = Convert.ToInt32(Session["UserId"]);
                ViewBag.Message = _repository.CreateUpdateNewAddress(tbladdressbook);
                //}

            }
            ModelState.Clear();
            CountryServices objcon = new CountryServices();
            ViewBag.country_code = objcon.Country();
            return View("SingleAddress");
        }
        [HttpPost]
        public ActionResult AddressBook(int id)
        {
            if (id != 0)
            {
                ViewBag.Message = _repository.DeleteSingleAddressByID(id);
            }
            return View(_repository.GetAddressBookData(null, false).ToList());
        }
        [HttpGet]
        public ActionResult AddressBook()
        {

            return View(_repository.GetAddressBookData(null, false).ToList());
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel ChangePasswordmodel)
        {
            if (ChangePasswordmodel != null)
            {
                ViewBag.message = _repository.ChangeUserPassword(ChangePasswordmodel).ToList();
                ModelState.Clear();
            }
            return View();
        }

        public JsonResult CheckOldPassword(string pass)
        {
            bool isMatch = false;
            if (pass != null)
            {
                isMatch = _repository.CheckPassword(pass);
            }
            return Json(isMatch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeEmail()
        {
            ChangeEmailId ch = new ChangeEmailId();
            var chemail = _repository.GetTblCustomerData();
            ch.currentemail = chemail.email;
            return View(ch);
        }
        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailId changeemailId)
        {
            if (changeemailId != null)
            {
                ViewBag.message = _repository.ChangeUserEmail(changeemailId).ToList();
                ModelState.Clear();
            }
            ChangeEmailId ch = new ChangeEmailId();
            var chemail = _repository.GetTblCustomerData();
            ch.currentemail = chemail.email;
            return View(ch);
        }
        public ActionResult SingleCreditCard()
        {
            CountryServices objcon = new CountryServices();
            ViewBag.country_code = objcon.Country();
            return View();
        }

        [HttpPost]
        public ActionResult SingleCreditCard(tblBillingAddress tblbillingaddress)
        {
            if (tblbillingaddress != null)
            {

                ViewBag.Message = _repository.CreateUpdateBillingAddress(tblbillingaddress);
            }
            ModelState.Clear();
            CountryServices objcon = new CountryServices();
            ViewBag.country_code = objcon.Country();
            return View("SingleCreditCard");
        }
        public ActionResult Referral()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Referral(ReferralEmailModel ReferralEmailModel)
        {
            ClsCommanCustomerSignup clscommn = new ClsCommanCustomerSignup();
            string subject = "Invitation to join 4InShip";
            string body = "Congratulations " + ReferralEmailModel.name + " !! You have been invited to join 4InShip and get discount on Signup, use the following link for Signup https://secure2.4InShip.com/shipping/signup?r=2e0d6 Thank you for using 4InShip.com.";
            Dictionary<string, string> dicTemplate = new Dictionary<string, string>();
            dicTemplate.Add("#header#", "4Inship - Referal Invitation");
            dicTemplate.Add("#content#", body);
            dicTemplate.Add("#email#", ReferralEmailModel.email);
            string mailbody = clscommn.Emailtemplate(dicTemplate);
            try
            {
                clscommn.SendEmail(ReferralEmailModel.email, ReferralEmailModel.email, body, subject);
                ViewBag.Message = "Referal email successfully send";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }

            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult ManagePoints()
        {
            RewardpointsandBalance();
            return View();
        }
        [HttpPost]
        public ActionResult ManagePoints(int a=1)
        {
           int Balance = Convert.ToInt32(ConfigurationManager.AppSettings["RewardPoints"]);
           var points= _repositoryshipment.yourpoints();
           var balance = Convert.ToInt32(points) / Balance;
            if (balance!=0)
            {
                ViewBag.Message = _repository.insertrewardpoint(balance);
            }
            RewardpointsandBalance();
            return View();
        }
        private void RewardpointsandBalance()
        {

            int Balance = Convert.ToInt32(ConfigurationManager.AppSettings["RewardPoints"]);
            ViewBag.yourcreditAmount = _repositoryshipment.yourBalance();
            ViewBag.yourpoints = _repositoryshipment.yourpoints();
            Balance = Balance * 100;
            ViewBag.yourbalance = Convert.ToInt32(ViewBag.yourpoints) / Balance;
           


        }
    }
}