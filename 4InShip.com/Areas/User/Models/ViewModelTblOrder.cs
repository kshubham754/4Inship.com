using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class ViewModelOrder
    {
        public int id { get; set; }
        public string reference_no { get; set; }
        public int Fk_customer_id { get; set; }
        public string invoice_refrence { get; set; }
        public string Delivery_Address { get; set; }
        public string carrier { get; set; }
        public string product { get; set; }
        public string service { get; set; }
        public Nullable<System.DateTime> pickup_date { get; set; }
        public Nullable<System.DateTime> pickup_cut_off_time { get; set; }
        public Nullable<System.DateTime> booking_time { get; set; }
        public Nullable<System.DateTime> delvery_date { get; set; }
        public Nullable<System.DateTime> delvery_time { get; set; }
        public Nullable<decimal> payable_amount { get; set; }
        public string tracking_no { get; set; }
        public decimal billing_weight { get; set; }
        public Nullable<bool> is_delivered { get; set; }
        public string signature { get; set; }
        public int status { get; set; }
    }
}