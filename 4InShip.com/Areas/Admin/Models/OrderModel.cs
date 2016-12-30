using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(OrderModelProperty))]
    public partial class tblOrderBox
    {


    }
    public class OrderModelProperty
    {
        public int id { get; set; }
        public int Fk_order_id { get; set; }
        [Required(ErrorMessage = "Actual Weight is required")]
        public decimal actual_weight { get; set; }
        [Required(ErrorMessage = "Dim Weight is required")]
        public decimal dim_weight { get; set; }
        [Required(ErrorMessage = "Billable Weight is required")]
        public Nullable<decimal> billable_weight { get; set; }
        [Required(ErrorMessage = "Height is required")]
        public Nullable<decimal> height { get; set; }
        [Required(ErrorMessage = "Width is required")]
        public Nullable<decimal> width { get; set; }
        [Required(ErrorMessage = "Length is required")]
        public Nullable<decimal> length { get; set; }
    }
}