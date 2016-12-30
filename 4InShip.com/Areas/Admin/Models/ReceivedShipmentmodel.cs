using _4InShip.com.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(tblReceivedShipmentproperties))]
    public partial class tblReceivedShipment
    {
        [Required(ErrorMessage = "Status is required")]
        public enumReceivShipmentStatus ItemType { get; set; }
        //ShipmentDetail Table properties use ----
        public int[] Quantity { get; set; }
        public string[] Description { get; set; }
        public int[] PurchasePrice { get; set; }
        public string[] ComodityCode { get; set; }
    }
    public class tblReceivedShipmentproperties
    {
        [Required(ErrorMessage = "Assigned Customer is required")]
        public int Fk_Customer_Id { get; set; }
        [Required(ErrorMessage = "Received Date is required")]
        public Nullable<System.DateTime> received_date { get; set; }
        [Required(ErrorMessage = "Sender is required")]
        public string sender { get; set; }
        [Required(ErrorMessage = "Tracking is required")]
        public string tracking { get; set; }
        [Required(ErrorMessage = "Carrier is required")]
        public string carrier { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public Nullable<decimal> weight { get; set; }
        [Required(ErrorMessage = "Height is required")]
        public Nullable<decimal> height { get; set; }
        [Required(ErrorMessage = "Length is required")]
        public Nullable<decimal> length { get; set; }
        [Required(ErrorMessage = "Box Condition is required")]
        public string box_condition { get; set; }
        [Required(ErrorMessage = "Staff Comments is required")]
        public string staff_comments { get; set; }
        public string user_notes { get; set; }
        public Nullable<bool> is_requested_picture { get; set; }

        public enumReceivShipmentStatus status { get; set; }
        public Nullable<System.DateTime> created_on { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> modified_on { get; set; }
        public string modified_by { get; set; }
        [Required(ErrorMessage = "Width is required")]
        public Nullable<decimal> width { get; set; }
        [Required(ErrorMessage = "Shelf_No is required")]
        public string shelf_no { get; set; }
        public string created_invoice_file_path { get; set; }

    }
}