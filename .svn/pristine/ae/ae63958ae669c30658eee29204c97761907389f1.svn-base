using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Services
{
    public class InvoiceService
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public string InsertUpdateTblInvoice(tblInvoice tblinvoice)
        {
            try
            {
                if (tblinvoice.Id != 0)
                {
                   
                    var tbl = Context.tblInvoices.Where(x => x.Id == tblinvoice.Id).FirstOrDefault();
                    if (tbl != null)
                    {
                        //tbl.Id = tblpackagingoption.Id;
                        //tbl.title = tblpackagingoption.title;
                        //tbl.description = tblpackagingoption.description;
                        //tbl.status = true;
                        //tbl.modified_on = DateTime.Now;
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                        return "Updated successfully";
                    }
                }
                else
                {
                    tblinvoice.invoice_date = DateTime.Now;
                    tblinvoice.paid_on = DateTime.Now;
                    Context.tblInvoices.Add(tblinvoice);
                    //  Context.Configuration.ValidateOnSaveEnabled = false;                 
                }
                Context.SaveChanges();
                return "Saved successfully";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public List<ViewModelInvoice> GetTblInvoiceDetail()
        {
            var InvoiveList = (from inv in Context.tblInvoices join cus in Context.tblCustomers on inv.Fk_customer_id equals cus.Id join bill in Context.tblBillingAddresses on inv.Fk_billing_address_id equals bill.Id  select new {cus.email ,cus.customer_reference, bill.address, inv.Id, inv.reference_no, inv.invoice_number, inv.invoice_date,inv.invoice_type,inv.transaction_status, inv.paid_status,inv.custom_guid, inv.paid_on, inv.payment_method, inv.payment_amount, inv.transaction_id, inv.transaction_response}).OrderByDescending(x=>x.invoice_date).ToList();
            //var Invoice = (from tblcust in Context.tblCustomers join tblinv in Context.tblInvoices on tblcust.Id equals tblinv.Fk_customer_id join tblbill in Context.tblBillingAddresses on tblinv.Fk_billing_address_id equals tblbill.Id select new { tblinv.reference_no, tblbill.address }).ToList();
            List<ViewModelInvoice> objtblInvoice= new List<ViewModelInvoice>();
            foreach (var item in InvoiveList)
            {
                objtblInvoice.Add(new ViewModelInvoice() {Id=item.Id,reference_no=item.reference_no,customer=item.customer_reference,billingaddress=item.address,invoicenumber=item.invoice_number,invoice_date=item.invoice_date,paid_status=item.paid_status,paid_on=item.paid_on,payment_method=item.payment_method,payment_amount=item.payment_amount,transaction_id=item.transaction_id,custom_guid=item.custom_guid,invoice_type=item.invoice_type,transaction_status=item.transaction_status,transaction_response=item.transaction_response,customeremail=item.email });

            }
            return objtblInvoice.ToList();
        }
    }
}