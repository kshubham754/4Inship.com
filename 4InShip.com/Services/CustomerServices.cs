using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4InShip.com.IServices;
using _4InShip.com.Models;
using Microsoft.Practices.Unity;
using _4InShip.com.Repository;
using System.Configuration;
using System.Text;
using System.Data.Entity;

namespace _4InShip.com.Services
{
    public class CustomerServices : Areas.Admin.Models.clsCommon, ICustomerServices<ViewCustomerModel, ViewCustomerPaymentModel>
    {
        [Dependency]
        public Context4InshipEntities Context { get; set; }

        public string CustomerSignUp(ViewCustomerModel objViewCustomerModel)
        {
            try
            {
                var CheckEmail = Context.tblCustomers.Where(x => x.email == objViewCustomerModel.email).Select(x => x.email).FirstOrDefault();
                string SignUpCouponCode = ConfigurationManager.AppSettings["SignUpCouponCode"].ToString();
                decimal SignUpCouponValue = Convert.ToDecimal(ConfigurationManager.AppSettings["SignUpCouponValue"]);
                int SignUpFreeTrialPeriod = Convert.ToInt32(ConfigurationManager.AppSettings["SignUpFreeTrialPeriod"]);
                DateTime CouponCodeTrailTo = Convert.ToDateTime(ConfigurationManager.AppSettings["SignUpMaxTrialDate"]);
                DateTime SignUpTrailCouponMonth = DateTime.Now.AddMonths(SignUpFreeTrialPeriod);
                DateTime AccountExpDate;
                if (SignUpTrailCouponMonth <= CouponCodeTrailTo)
                {
                    AccountExpDate = SignUpTrailCouponMonth;
                }
                else
                {
                    AccountExpDate = CouponCodeTrailTo;
                }

                if (CheckEmail == null && objViewCustomerModel.email != null && objViewCustomerModel.password != null && objViewCustomerModel.Address != null)
                {
                    tblCustomer objtblCustomer = new tblCustomer();
                    ClsCommanCustomerSignup objClsCommanCustomerSignup = new ClsCommanCustomerSignup();
                    objtblCustomer.email = objViewCustomerModel.email;
                    objtblCustomer.password = objViewCustomerModel.password;
                    objtblCustomer.account_expiry_date = AccountExpDate;
                    objtblCustomer.created_on = DateTime.Now;
                    objtblCustomer.status = true;
                    var CheckCustmorPlan = Context.tblPlans.Where(x => x.Id == objViewCustomerModel.Plan_id).Select(x => x.price).SingleOrDefault();
                    decimal CoupcodeAmountStatus = 0;
                    if (objViewCustomerModel.CuponCode == SignUpCouponCode)
                    {
                        CoupcodeAmountStatus = Convert.ToDecimal(CheckCustmorPlan) - SignUpCouponValue;
                    }
                    else
                        CoupcodeAmountStatus = SignUpCouponValue;

                    if (CoupcodeAmountStatus == 0)
                    {
                        string referenceNumber = objClsCommanCustomerSignup.GetCustomerReference();
                        objtblCustomer.customer_reference = referenceNumber;
                        Context.tblCustomers.Add(objtblCustomer);
                        Context.SaveChanges();
                        objClsCommanCustomerSignup.tblcustomerLinking(objViewCustomerModel);
                        objClsCommanCustomerSignup.tblAddressBookSaveData(objViewCustomerModel);
                        objClsCommanCustomerSignup.SaveBillingAdrress();
                        objClsCommanCustomerSignup.tblSignCouponConsume(SignUpCouponCode, SignUpCouponValue, objViewCustomerModel);
                        string MailSubject = "You has been successfully registered";
                        Dictionary<string, string> dicTemplate = new Dictionary<string, string>();
                        dicTemplate.Add("#header#", "4Inship - SignUp");
                        dicTemplate.Add("#content#", $"Username : {objtblCustomer.email} <br/> Password : {objtblCustomer.password}");
                        dicTemplate.Add("#email#", objtblCustomer.email);
                        string body = objClsCommanCustomerSignup.Emailtemplate(dicTemplate);
                        objClsCommanCustomerSignup.SendEmail(objtblCustomer.email, objtblCustomer.email, body, MailSubject);
                        return "Register successfully redirect login page";
                    }
                    else
                    {
                        HttpSessionStateBase Session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                        string MailSubject = "You has been successfully registered";
                        Dictionary<string, string> dicTemplate = new Dictionary<string, string>();
                        dicTemplate.Add("#header#", "4Inship - SignUp");
                        dicTemplate.Add("#content#", $"Username : {objtblCustomer.email} <br/> Password : {objtblCustomer.password}");
                        dicTemplate.Add("#email#", objtblCustomer.email);
                        string body = objClsCommanCustomerSignup.Emailtemplate(dicTemplate);
                        objClsCommanCustomerSignup.SendEmail(objtblCustomer.email, objtblCustomer.email, body, MailSubject);
                        objtblCustomer.account_expiry_date = null;
                        string referenceNumber = objClsCommanCustomerSignup.GetCustomerReference();
                        objtblCustomer.customer_reference = referenceNumber;
                        Context.tblCustomers.Add(objtblCustomer);
                        Context.SaveChanges();
                        objClsCommanCustomerSignup.tblcustomerLinking(objViewCustomerModel);
                        objClsCommanCustomerSignup.tblAddressBookSaveData(objViewCustomerModel);
                        objClsCommanCustomerSignup.tblSignCouponConsume(SignUpCouponCode, SignUpCouponValue, objViewCustomerModel);
                        var Customer_ID = Context.tblCustomers.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                        string CustomerID = Cryptography.Encrypt(Convert.ToString(Customer_ID));
                        Session["customerDycriptID"] = CustomerID;
                        return CustomerID;
                    }


                }
                else
                {
                    return ErrorMsg("User already exist!");
                }
            }
            catch (Exception ex)
            {

                return ErrorMsg(ex.Message.ToString());
            }

        }
        public ViewCustomerPaymentModel GetAddressBookData(int ID)
        {
            try
            {
                tblCustomer objtblCustomer = new tblCustomer();
                var CustomerEmail = Context.tblCustomers.Where(x => x.Id == ID).Select(x => x.email).FirstOrDefault();
                var AdressBookData = Context.tblAddressBooks.Where(x => x.Fk_customer_Id == ID).Select(x => x).FirstOrDefault();
                objtblCustomer.Id = ID;
                ViewCustomerPaymentModel objViewCustomerPaymentModel = new ViewCustomerPaymentModel();
                if (AdressBookData != null)
                {
                    objViewCustomerPaymentModel.first_name = AdressBookData.first_name;
                    objViewCustomerPaymentModel.last_name = AdressBookData.last_name;
                    objViewCustomerPaymentModel.email = CustomerEmail;
                    objViewCustomerPaymentModel.city = AdressBookData.city;
                    objViewCustomerPaymentModel.mobile = AdressBookData.mobile1;
                    objViewCustomerPaymentModel.address = AdressBookData.address1;
                    objViewCustomerPaymentModel.Country = AdressBookData.country_code;
                    objViewCustomerPaymentModel.state = AdressBookData.state;
                    objViewCustomerPaymentModel.post_code = AdressBookData.post_code;
                }
                return objViewCustomerPaymentModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ViewCustomerModel GetTblCustomerIdByUserNamePassword(ViewCustomerModel tblcustomer)
        {
            try
            {
                var Data = Context.tblCustomers.SingleOrDefault(x => x.email == tblcustomer.email && x.password == tblcustomer.password && x.status == true);
                if (Data != null)
                {
                    var GetAccountExpDate = Context.tblCustomers.Where(x => x.email == Data.email).Select(x => x.email).FirstOrDefault();
                    tblcustomer.email = Data.email;
                    tblcustomer.Id = Data.Id;
                    tblcustomer.AccountExpDate = Convert.ToString(Data.account_expiry_date);
                    tblcustomer.password = Data.password;
                    return tblcustomer;
                }
                else
                {
                    return tblcustomer = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string GetTblCustomerIdByUserNamePasswords(string emails)
        {
            try
            {
                var data = Context.tblCustomers.AsQueryable().Where(x => x.email == emails).FirstOrDefault();
                if (data != null)
                {
                    return data.email;
                }
                return null;

            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message.ToString());
            }

        }
        public string UpdateUserEmailPassword(string emails)
        {
            try
            {
                var useremail = Context.tblCustomers.AsQueryable().Where(x => x.email == emails).FirstOrDefault();
                return useremail.password;
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message.ToString());
            }


        }
        public string CustomerPayBillingAddress(ViewCustomerPaymentModel objViewCustomerPaymentModel)
        {
            try
            {
                tblBillingAddress objbillingaddress = new tblBillingAddress();
                objbillingaddress.first_name = objViewCustomerPaymentModel.first_name;
                objbillingaddress.last_name = objViewCustomerPaymentModel.last_name;
                objbillingaddress.mobile = objViewCustomerPaymentModel.mobile;
                objbillingaddress.address = objViewCustomerPaymentModel.address;
                objbillingaddress.country_code = objViewCustomerPaymentModel.Country;
                objbillingaddress.city = objViewCustomerPaymentModel.city;
                objbillingaddress.post_code = objViewCustomerPaymentModel.post_code;
                objbillingaddress.state = objViewCustomerPaymentModel.state;
                objbillingaddress.email = objViewCustomerPaymentModel.email;
                objbillingaddress.created_on = DateTime.Now;
                Context.tblBillingAddresses.Add(objbillingaddress);
                Context.SaveChanges();
                tblBillingAddress objtblBillingAddress = Context.tblBillingAddresses.OrderByDescending(x => x.Id).Select(x => x).FirstOrDefault();
                //tblSignupCouponConsumed objtblSignupCouponConsumed = Context.tblSignupCouponConsumeds.Where(x => x.Fk_CustomerId == objViewCustomerPaymentModel.Fk_customer_id).Select(x => x).FirstOrDefault();
                //tblCustomerPlanLinking objtblCustomerPlanLinking = Context.tblCustomerPlanLinkings.Where(x => x.Fk_Customer_Id == objViewCustomerPaymentModel.Fk_customer_id).Select(x => x).FirstOrDefault();
                //decimal PlanAmount = objtblCustomerPlanLinking.plan_amount;
                //decimal couponAmount = Convert.ToDecimal(objtblSignupCouponConsumed.redeemed_Amount);
                //decimal PaymentAmount = PlanAmount - couponAmount;
                decimal CustomerPaidAmount = Context.tblCustomerPlanLinkings.Where(x => x.Fk_Customer_Id == objViewCustomerPaymentModel.Fk_customer_id).Select(x => x.plan_amount).FirstOrDefault();
                tblInvoice objInvoice = new tblInvoice();
                string referncenumber = Guid.NewGuid().ToString().Substring(0, 8);
                objInvoice.reference_no = referncenumber;
                objInvoice.Fk_customer_id = objViewCustomerPaymentModel.Fk_customer_id;
                objInvoice.Fk_billing_address_id = objtblBillingAddress.Id;
                string invoicenumber  = Guid.NewGuid().ToString().Substring(0, 8);
                objInvoice.invoice_number = invoicenumber;
                objInvoice.invoice_date = DateTime.Now;
                objInvoice.payment_amount = CustomerPaidAmount;
                objInvoice.paid_status = true;
                objInvoice.paid_on = DateTime.Now;
                objInvoice.payment_method = "Creadit Card";
                objInvoice.transaction_id = 1;
                objInvoice.invoice_type = "Signup";
                objInvoice.transaction_status = 1;
                objInvoice.transaction_response = "ok";
                Context.Configuration.ValidateOnSaveEnabled = false;
                Context.tblInvoices.Add(objInvoice);
                Context.SaveChanges();
                //  Save Data in creadit card tbl//
                
                tblCreditCard objcreditcard = new tblCreditCard();
                objcreditcard.Fk_customer_Id = objViewCustomerPaymentModel.Fk_customer_id;
                objcreditcard.card_type = "Master Card";
                objcreditcard.card_num = objViewCustomerPaymentModel.CreditCard;
                objcreditcard.card_expiry = DateTime.Now.AddYears(10).ToString();
                objcreditcard.cust_name = objViewCustomerPaymentModel.first_name + " " + objViewCustomerPaymentModel.last_name;
                objcreditcard.cust_address = objViewCustomerPaymentModel.address;
                objcreditcard.cust_post_code = objViewCustomerPaymentModel.post_code;
                objcreditcard.cust_email = objViewCustomerPaymentModel.email;
                objcreditcard.cust_mobile = objViewCustomerPaymentModel.mobile;
                objcreditcard.cust_country_code = objViewCustomerPaymentModel.Country;
                objcreditcard.created_on = DateTime.Now;
                objcreditcard.card_auth_id = "1100134876";
                objcreditcard.card_auth_status = "transection successfully";
                objcreditcard.card_auth_response = "Success";
                Context.tblCreditCards.Add(objcreditcard);
                Context.SaveChanges();
                tblCustomer objtblCustomer = Context.tblCustomers.Where(x => x.Id == objViewCustomerPaymentModel.Fk_customer_id).Select(x => x).FirstOrDefault();
                objtblCustomer.account_expiry_date = DateTime.Now;
                Context.Entry(objtblCustomer).State = EntityState.Modified;
                Context.SaveChanges();
                return "showMessage('You has been successfully registered',true)";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}