using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{
    public class OrderService : clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public string InsertUpdateOrder(tblOrder Order)
        {
            try
            {
                if (Order.id != 0)
                {
                    var Ids = Order.id;
                    var tbl = Context.tblOrders.AsQueryable().Where(x => x.id == Ids).FirstOrDefault();
                    if (tbl != null)
                    {
                        tbl.id = Order.id;
                        tbl.reference_no = Order.reference_no;
                        tbl.Fk_customer_id = Order.Fk_customer_id;
                        tbl.Fk_invoice_id = Order.Fk_invoice_id;
                        tbl.carrier = Order.carrier;
                        tbl.product = Order.product;
                        tbl.service = Order.service;
                        tbl.pickup_date = Order.pickup_date;
                        tbl.pickup_cut_off_time = Order.pickup_cut_off_time;
                        tbl.booking_time = Order.booking_time;
                        tbl.delvery_date = Order.delvery_date;
                        tbl.delvery_time = Order.delvery_time;
                        tbl.payable_amount = Order.payable_amount;
                        tbl.tracking_no = Order.tracking_no;
                        tbl.billing_weight = Order.billing_weight;
                        tbl.is_delivered = Order.is_delivered;
                        tbl.signature = Order.signature;
                        Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                        return SuccessMsg("Updated successfully");
                    }
                }
                else
                {
                    Context.tblOrders.Add(Order);

                }
                Context.SaveChanges();
                return ErrorMsg("Saved successfully");
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public List<tblOrder> GetTblOrderById(int Id)
        {
            var lst = (from list in Context.tblOrders.AsQueryable().Where(x => x.id == Id) select list).ToList();
            return lst.ToList();
        }

        public List<ViewModelOrder> GetOrder()
        {
            try
            {
                var OrderList = (from ord in Context.tblOrders join cus in Context.tblCustomers on ord.Fk_customer_id equals cus.Id join inv in Context.tblInvoices on ord.Fk_invoice_id equals inv.Id select new { cus.Id, cus.customer_reference, ord.id, ord.reference_no, ord.Fk_invoice_id, ord.carrier, ord.product, ord.service, ord.pickup_date, ord.pickup_cut_off_time, ord.booking_time, ord.delvery_date, ord.delvery_time, ord.payable_amount, ord.tracking_no, ord.billing_weight, ord.is_delivered, ord.signature, ord.status, invref = inv.reference_no }).OrderByDescending(x => x.id).ToList();
                List<ViewModelOrder> objOrder = new List<ViewModelOrder>();
                foreach (var item in OrderList)
                {
                    objOrder.Add(new ViewModelOrder() { id = item.id, Customer_reference = item.customer_reference, reference_no = item.reference_no, carrier = item.carrier, product = item.product, service = item.service, pickup_date = item.pickup_date, pickup_cut_off_time = item.pickup_cut_off_time, booking_time = item.booking_time, delvery_date = item.delvery_date, delvery_time = item.delvery_time, payable_amount = Convert.ToString(item.payable_amount), tracking_no = item.tracking_no, billing_weight = item.billing_weight, is_delivered = item.is_delivered, signature = item.signature, Fk_invoice_id = Convert.ToInt32(item.Fk_invoice_id), invoice_Refernce = item.invref, status = Convert.ToInt32(item.status) });

                }
                return objOrder.ToList();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return null;
            }
        }
        public string DeletetblTblOrder(int Id)
        {
            try
            {
                var itemToRemove = Context.tblOrders.AsQueryable().SingleOrDefault(x => x.id == Id);
                if (itemToRemove != null)
                {
                    Context.Entry(itemToRemove).State = System.Data.Entity.EntityState.Deleted;
                }
                Context.SaveChanges();
                return SuccessMsg("Deleted successfully");
            }
            catch (Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
        }
        public string CreateOrderBox(tblOrderBox tblorderbox)
        {
            try
            {
                if (tblorderbox.id!=0)
                {
                }
                else
                {
                    Context.tblOrderBoxes.Add(tblorderbox);
                }
                Context.SaveChanges();
                return SuccessMsg("Order box has been created successfully");
            }
            catch(Exception ex)
            {
                return ErrorMsg(ex.Message);
            }
        }
    }
}