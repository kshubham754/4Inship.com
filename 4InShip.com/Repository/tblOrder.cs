//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _4InShip.com.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrder
    {
        public int id { get; set; }
        public string reference_no { get; set; }
        public int Fk_customer_id { get; set; }
        public Nullable<int> Fk_invoice_id { get; set; }
        public Nullable<int> Fk_delivery_address_id { get; set; }
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
        public Nullable<System.DateTime> creted_on { get; set; }
    }
}
