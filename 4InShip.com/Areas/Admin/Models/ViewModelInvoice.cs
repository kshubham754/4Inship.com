using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Models
{
    public class ViewModelInvoice
    {
        public int Id { get; set; }
        public string reference_no { get; set; }
        public string customer { get; set; }
        public string customeremail { get; set; }
        public string billingaddress { get; set; }
        public string invoicenumber { get; set; }
        public System.DateTime invoice_date { get; set; }
        public bool paid_status { get; set; }
        public System.DateTime paid_on { get; set; }
        public string payment_method { get; set; }
        public decimal payment_amount { get; set; }
        public int transaction_id { get; set; }
        public Nullable<System.Guid> custom_guid { get; set; }
        public string invoice_type { get; set; }
        public short transaction_status { get; set; }
        public string transaction_response { get; set; }
    }
}