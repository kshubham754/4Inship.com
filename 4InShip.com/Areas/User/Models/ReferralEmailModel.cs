using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class ReferralEmailModel
    {
        [Required(ErrorMessage = "Email Cannot be Blank")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string email { get; set;}
        [Required(ErrorMessage = "Name Cannot be Blank")]
        public string name { get; set; }
    }
}