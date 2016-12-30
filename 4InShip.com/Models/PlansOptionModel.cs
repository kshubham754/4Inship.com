using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Models
{
    public class PlansOptionModel
    {
        public int p_id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public bool IsSignUp { get; set; }
    }
}