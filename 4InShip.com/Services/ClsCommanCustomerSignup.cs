using _4InShip.com.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace _4InShip.com.Services
{
    public class ClsCommanCustomerSignup
    {
        Context4InshipEntities Context = new Context4InshipEntities();

        public void SaveBillingAdrress()
        {
            try
            {
                var tblAddressBooksAddress = Context.tblAddressBooks.OrderByDescending(x => x.Id).FirstOrDefault();
                var customerEmail = Context.tblCustomers.OrderByDescending(x => x.Id).FirstOrDefault();
                tblBillingAddress objtblBillingAddress = new tblBillingAddress();
                objtblBillingAddress.email = customerEmail.email;
                objtblBillingAddress.first_name = tblAddressBooksAddress.first_name;
                objtblBillingAddress.last_name = tblAddressBooksAddress.last_name;
                objtblBillingAddress.mobile = tblAddressBooksAddress.mobile1;
                objtblBillingAddress.address = tblAddressBooksAddress.address1;
                objtblBillingAddress.country_code = tblAddressBooksAddress.country_code;
                objtblBillingAddress.state = tblAddressBooksAddress.state;
                objtblBillingAddress.country_code = tblAddressBooksAddress.country_code;
                objtblBillingAddress.city = tblAddressBooksAddress.city;
                objtblBillingAddress.post_code = tblAddressBooksAddress.post_code;
                objtblBillingAddress.created_on = DateTime.Now;
                Context.tblBillingAddresses.Add(objtblBillingAddress);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void tblAddressBookSaveData(ViewCustomerModel objtbAddressBook)
        {
            try
            {
                var fk_customer_ID = Context.tblCustomers.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault();
                tblAddressBook objtblAddressBook = new tblAddressBook();
                objtblAddressBook.address_name = "My Account Address";
                objtblAddressBook.Fk_customer_Id = fk_customer_ID;
                objtblAddressBook.first_name = objtbAddressBook.FirstName;
                objtblAddressBook.last_name = objtbAddressBook.LastName;
                objtblAddressBook.address1 = objtbAddressBook.Address;
                objtblAddressBook.mobile1 = objtbAddressBook.MobileNumber;
                objtblAddressBook.country_code = objtbAddressBook.Country;
                objtblAddressBook.city = objtbAddressBook.City;
                objtblAddressBook.state = objtbAddressBook.Address;
                objtblAddressBook.post_code = objtbAddressBook.ZipCode;
                objtbAddressBook.Company = objtbAddressBook.Company;
                objtblAddressBook.is_default = true;
                objtblAddressBook.created_on = DateTime.Now;
                Context.Configuration.ValidateOnSaveEnabled = false;
                Context.tblAddressBooks.Add(objtblAddressBook);
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }
        public void tblcustomerLinking(ViewCustomerModel objViewCustomerModel)
        {
            try
            {
                var fk_customer_ID = Context.tblCustomers.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault();
                tblCustomerPlanLinking objtblCustomerPlanLinking = new tblCustomerPlanLinking();
                var list = (from planlist in Context.tblPlans.Where(x => x.Id == objViewCustomerModel.Plan_id) select planlist).SingleOrDefault();
                objtblCustomerPlanLinking.Fk_Customer_Id = fk_customer_ID;
                objtblCustomerPlanLinking.plan_amount = Convert.ToDecimal(list.price);
                objtblCustomerPlanLinking.plan_title = list.title;
                objtblCustomerPlanLinking.created_on = DateTime.Now;
                Context.tblCustomerPlanLinkings.Add(objtblCustomerPlanLinking);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void tblSignCouponConsume(string SignUpCouponCode, decimal SignUpCouponValue, ViewCustomerModel objViewCustomerModel)
        {
            tblSignupCouponConsumed objtblSignupCouponConsumed = new tblSignupCouponConsumed();
            try
            {
                if (SignUpCouponCode == objViewCustomerModel.CuponCode && objViewCustomerModel.CuponCode != null)
                {
                    var fk_customer_ID = Context.tblCustomers.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault();
                    var CustPlanLink_id = Context.tblCustomerPlanLinkings.Select(x => x.Fk_Customer_Id).OrderByDescending(x => x).FirstOrDefault();
                    objtblSignupCouponConsumed.Fk_CustomerId = fk_customer_ID;
                    objtblSignupCouponConsumed.redeemed_Amount = SignUpCouponValue;
                    objtblSignupCouponConsumed.coupon_Code = objViewCustomerModel.CuponCode;
                    objtblSignupCouponConsumed.redeemed_on = DateTime.Now;
                    Context.tblSignupCouponConsumeds.Add(objtblSignupCouponConsumed);
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     public string SendEmail(string to, string replyTo, string body, string subject)
        {
            string returnString = "";
            try
            {
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                // draft the email
                string Clientmail = ConfigurationManager.AppSettings["ClentMail"].ToString();
                email.To.Add(new MailAddress(to));
                email.From = new MailAddress(Clientmail);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;
                smtp.Send(email);

                returnString = "Email Successfully Send";
            }
            catch (Exception ex)
            {
                returnString = "Error: " + ex.ToString();
            }
            return returnString;
        }
        public string GetCustomerReference()
        {
            try
            {

                string strCustRefe = Convert.ToString(Context.tblCustomers.AsQueryable().OrderByDescending(x => x.Id).Select(x => x.customer_reference).FirstOrDefault());
                if (strCustRefe == null)
                {
                    return "001-001";
                }
                string[] aryCustRefe = strCustRefe.Split('-');
                int secondNumber = Convert.ToInt32(aryCustRefe[1]) + 1;
                return (secondNumber == 1000 ? (Convert.ToInt32(aryCustRefe[0]) + 1).ToString().PadLeft(3, '0') + "-001" : aryCustRefe[0] + "-" + secondNumber.ToString().PadLeft(3, '0'));
            }

            catch(Exception ex)
            {
                return ex.Message;

            }
            }
            
        public string Emailtemplate(Dictionary<string, string> dic)
        {
            string template = @"
<html>
<head>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>
<body>
    <div style=""float: left;margin: 53px 0 0;width: 100%;"">
        <div style=""background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
            margin: auto;width: 50%;border-color: #63b24b !important; min-height: 239px !important; border-radius: none !important;"">
            <div style=""border-bottom: 1px solid transparent;border-top-left-radius: 3px;border-top-right-radius: 3px;padding: 10px 15px;background-color: #d6351e !important; color: #fff;font-size: 18px;"""">
                #header#
            </div>
            <div style=""padding: 15px;"">
                <div>
                    <p style=""font-size: 14px; line-height: 26px;"">
                        #content#<br>
                    </p>
                </div>
            </div>

             </div>
    </div>
</body>
</html>";
            foreach (KeyValuePair<string, string> keyvalue in dic)
            {
                template = template.Replace(keyvalue.Key, keyvalue.Value);
            }
            return template;
        }
    }
}