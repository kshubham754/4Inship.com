using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _4InShip.com.Models
{
    public class ViewCustomerPaymentModel
    {
        public int Fk_customer_id { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string last_name { get; set; }
        public string email { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string state { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        public string post_code { get; set; }
        [Required(ErrorMessage = "Credit Card is required")]
        public string CreditCard { get; set; }
        [Required(ErrorMessage = "Expire Month is required")]
        public string ExpireMonth { get; set; }
        [Required(ErrorMessage = "Expire Year is required")]
        public string ExpireYear { get; set; }
        [Required(ErrorMessage = "CVV Code is required")]
        public string CVCCode { get; set; }
        public System.DateTime created_on { get; set; }
    }
}