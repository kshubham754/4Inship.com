using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{
    public class ReturnPackagesService
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public List<View_ModelReturnPackage> GettblShipmentReturnDetail()
        {
            var returnList = (from ret in Context.tblShipmentReturnDetails join rec in Context.tblReceivedShipments  on ret.Fk_shipment_id equals rec.id join cust in Context.tblCustomers on rec.Fk_Customer_Id equals cust.Id select new { cust.customer_reference ,rec.sender,rec.tracking,rec.carrier,ret.first_name,ret.last_name,ret.address1,ret.address2,ret.city,ret.state,ret.postal_code,ret.mobile,ret.rma,ret.prepaid_label_filename,id=ret.id}).OrderByDescending(x=>x.id).ToList();
            List<View_ModelReturnPackage> objviewmodel = new List<View_ModelReturnPackage>();
            foreach (var item in returnList)
            {
                objviewmodel.Add(new View_ModelReturnPackage() { customer_reference = item.customer_reference, id = item.id, sender = item.sender, tracking = item.tracking, carrier = item.carrier, first_name = item.first_name, last_name = item.last_name, address1 = item.address1, address2 = item.address2, city = item.city, state = item.state, postal_code = item.postal_code, mobile = item.mobile, rma = item.rma, prepaid_label_filename = item.prepaid_label_filename});

            }
            return objviewmodel.ToList();
        }
    }
}