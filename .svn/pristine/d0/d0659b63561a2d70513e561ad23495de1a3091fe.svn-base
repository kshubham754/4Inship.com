using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class ViewRecivedShipmentModel: ReturnPackageModel
    {
        public int ID { get; set; }
        public DateTime received_date { get; set; }
        public string sender { get; set; }
        public string tracking { get; set; }
        public string carrier { get; set; }
        public decimal ? weight { get; set; }
        public decimal ? height { get; set; }
        public decimal ?length { get; set; }
        public decimal ? width { get; set; }
        public  string box_condition { get; set; }
        public string staff_comments { get; set; }
        public string user_notes { get; set; }
        public int status { get; set; }
        public int  quantity { get; set; }
        public int Fk_shipment_id { get; set; }
        public decimal purchase_price { get; set; }
        public string description { get; set; }
        public int ? StorageDays { get; set; }
        public List<dynamic>RecivedShipment_ImagePath { get; set; }
    }
    public class ReturnPackageModel
    {
        public string[] returnpackageid { get; set; }
        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address1 is required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip is required")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        public string Rma { get; set; }
        [Required(ErrorMessage = "Please upload a file")]
        public string ReturnpackagefileUpload { get; set; }
    }
}