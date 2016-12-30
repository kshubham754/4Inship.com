using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Repository;
using _4InShip.com.Models;

namespace _4InShip.com.Areas.User.Models
{
    public class UserCommanClass
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public List<SelectListItem> AddressTableDrpdown(int ID)
        {
            try
            {
                var AddressList = Context.tblAddressBooks.Where(x => x.is_default == true && x.Fk_customer_Id == ID).ToList();
                List<SelectListItem> ObjAddressTableList = new List<SelectListItem>();
                foreach (var item in AddressList)
                {
                    ObjAddressTableList.Add(new SelectListItem { Value = Convert.ToString(item.Id), Text = item.address_name });
                }
                return ObjAddressTableList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectListItem> ShipmentCarrierApi(int id)
        {
            try
            {
                List<SelectListItem> Shipmentcarrier = new List<SelectListItem>();
                Shipmentcarrier.Add(new SelectListItem { Text = "DHL Express", Value = "$ 66.67" });
                Shipmentcarrier.Add(new SelectListItem { Text = "UPS Express", Value = "$ 50.67" });
                Shipmentcarrier.Add(new SelectListItem { Text = "DHL Express", Value = "$ 40.67" });
                return Shipmentcarrier.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public class ShipmentMethodPackage
    {
        public string shipmentMethodPrice { get; set; }
        public string shipmentdescription { get; set; }

    }

    public class PacakageOptionArray
    {
        public string PackageOptionPrice { get; set; }
        public string PackageOptionTitle { get; set; }

    }
}