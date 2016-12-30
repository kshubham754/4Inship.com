using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Models
{
    public class ViewModelreceivedShipment
    {
        public int id { get; set; }
        public List<dynamic> chkimage { get; set; }
        public int temp { get; set; }
        public string Fk_assigned_user { get; set; }
        public System.DateTime received_date { get; set; }
        public string sender { get; set; }
        public string tracking { get; set; }
        public string carrier { get; set; }
        public Nullable<decimal> weight { get; set; }
        public Nullable<decimal> height { get; set; }
        public Nullable<decimal> length { get; set; }
        public Nullable<decimal> width { get; set; }
        public string box_condition { get; set; }
        public string staff_comments { get; set; }
        
        public string status { get; set; }
        public Nullable<System.DateTime> created_on { get; set; }
       
        public Nullable<System.DateTime> modified_on { get; set; }

        public Nullable<bool> is_requested_picture { get; set; }
        public string shelf_no { get; set; }
    }
}