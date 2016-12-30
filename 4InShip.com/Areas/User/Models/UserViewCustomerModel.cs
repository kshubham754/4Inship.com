using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class UserViewCustomerModel
    {
        public string PlanTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       public decimal PlanPrice { get; set; }
    }
}