using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _4InShip.com.Models
{
    public class ViewCustomerModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username/Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter a valid e-mail adress")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [CompareAttribute("password", ErrorMessage = "Your password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNumber { get; set; }
        public string Company { get; set; }
        public string CuponCode { get; set; }
        public string AccountExpDate { get; set; }
        public int Plan_id { get; set; }

    }
}