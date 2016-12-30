using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.User.Services
{
    public class OrderService
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public List<ViewModelOrder> GettblShipmentReturnDetail()
        {
            var OrderList = (from ord in Context.tblOrders join inv in Context.tblInvoices on ord.Fk_invoice_id equals inv.Id join dilad in Context.tblDeliveryAddresses on ord.Fk_delivery_address_id equals dilad.id  join link in Context.tblCustomerPlanLinkings on ord.Fk_customer_id equals link.Id  select new {inv.reference_no,dilad.country_code,ord.id, ordref=ord.reference_no,ord.Fk_customer_id,car=ord.carrier,ord.product,ord.service,ord.pickup_date,ord.pickup_cut_off_time,ord.booking_time,ord.delvery_time,ord.delvery_date,ord.payable_amount,ord.tracking_no,ord.billing_weight,ord.is_delivered,ord.signature,ord.status ,dev=ord.delvery_date,ord.creted_on,link .free_storage_days}).OrderByDescending(x=>x.creted_on).ToList();
            List<ViewModelOrder> objviewmodel = new List<ViewModelOrder>();
            foreach (var item in OrderList)
            {
                 int Checkstatus = item.status;
                enumorder enumDisplayStatus = (enumorder)Checkstatus;
                string stringValue = enumDisplayStatus.ToString();
                objviewmodel.Add(new ViewModelOrder() {id=item.id,reference_no=item.ordref, Fk_customer_id=item.Fk_customer_id,delivery_Address=item.country_code,car=item.car,product=item.product,service=item.service,pickup_date=item.pickup_date,pickup_cut_off_time=item.pickup_cut_off_time,booking_time=item.booking_time,delvery_date=item.dev,delvery_time=item.delvery_time,payable_amount=Convert.ToString(item.payable_amount),tracking_no=item.tracking_no,billing_weight=item.billing_weight,is_delivered=item.is_delivered,signature=item.signature,status=Convert.ToInt32(stringValue),invoice_Refernce=item.reference_no,freeleftDays=Convert.ToString(item.free_storage_days),createdOn=item.creted_on});

            }
            return objviewmodel.ToList();
        }
    }
}