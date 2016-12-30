﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class ViewOrderModel
    {
        public int Fk_Customer_id { get; set; }
        public int Invoice_id { get; set; }
        public int Order_id { get; set; }
        public int Fk_orderAddres_id { get; set; }
        public string RefernceNumber { get; set; }
        public string Carrier { get; set; }
        public string Product { get; set; }
        public string service { get; set; }
        public DateTime Pickup_Date { get; set; }
        public DateTime Pick_Cutoff_Date { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime DeleviredDate { get; set; }
        public decimal? PaybelAmount { get; set; }
        public string TrackingNumber { get; set; }
        public decimal ? Billing_Weight { get; set; }
        public bool Is_Deliverd { get; set; }
        public string Signature { get; set; }
        public int Status { get; set; }
        public DateTime Created_on { get; set; }
        public string[] ShipmentCheckboxID { get; set; }
        //tbl OrderCharges//
        public int Fk_order_id { get; set; }
        public string ChargesName { get; set; }
        public decimal ? Buy_Charges { get; set; }
        public decimal ? Sell_Price { get; set; }
       
            

    }
}