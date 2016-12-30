using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using _4InShip.com.Areas.Admin.Models;
using System.Web;
using EntityFramework.BulkInsert.Extensions;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.IO;
namespace _4InShip.com.Areas.Admin.Services
{
    public class ReceivedShipmentService : clsCommon
    {
        Context4InshipEntities Context = new Context4InshipEntities();
        public object ViewBag { get; private set; }
        public string InsertUpdateReceivedShipment(tblReceivedShipment receivedshipment)
        {
            try
            {
                byte st = Convert.ToByte(receivedshipment.ItemType);
                if (receivedshipment.id != 0)
                {
                    var Ids = receivedshipment.id;
                    var tbl = Context.tblReceivedShipments.AsQueryable().Where(x => x.id == Ids).FirstOrDefault();
                    if (tbl != null)
                    {
                        tbl.id = receivedshipment.id;
                        tbl.Fk_Customer_Id = receivedshipment.Fk_Customer_Id;
                        tbl.received_date = receivedshipment.received_date;
                        tbl.sender = receivedshipment.sender;
                        tbl.tracking = receivedshipment.tracking;
                        tbl.carrier = receivedshipment.carrier;
                        tbl.weight = Convert.ToDecimal(receivedshipment.weight);
                        tbl.height = Convert.ToDecimal(receivedshipment.height);
                        tbl.length = Convert.ToDecimal(receivedshipment.length);
                        tbl.box_condition = receivedshipment.box_condition;
                        tbl.staff_comments = receivedshipment.staff_comments;
                        tbl.modified_by = HttpContext.Current.Session["AdminUsername"].ToString();
                        tbl.modified_on = DateTime.Now;
                        tbl.width = Convert.ToDecimal(receivedshipment.width);
                        tbl.received_date = receivedshipment.received_date;
                        tbl.status = st;
                        Context.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                        Context.SaveChanges();
                        var ShipList = Context.tblShipmentDetails.Where(x => x.Fk_shipment_id == tbl.id).Select(x => x).ToList();
                        Context.tblShipmentDetails.RemoveRange(Context.tblShipmentDetails.Where(x => x.Fk_shipment_id == tbl.id));
                        Context.SaveChanges();
                        List<tblShipmentDetail> objShipmentList = new List<tblShipmentDetail>();
                        for (int i = 0; i < receivedshipment.Description.Length; i++)
                        {
                            objShipmentList.Add(new tblShipmentDetail() { purchase_price = receivedshipment.PurchasePrice[i], quantity = receivedshipment.Quantity[i], comodity_code = receivedshipment.ComodityCode[i], description = receivedshipment.Description[i], Fk_shipment_id = tbl.id });
                        }
                        Context.BulkInsert(objShipmentList);
                        Context.SaveChanges();
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        return SuccessMsg("Updated successfully");
                    }
                }
                else
                {
                    receivedshipment.Fk_order_id = 1;
                    receivedshipment.created_on = DateTime.Now;
                    receivedshipment.created_by =HttpContext.Current.Session["AdminUsername"].ToString();
                    receivedshipment.status = st;
                    Context.tblReceivedShipments.Add(receivedshipment);
                    Context.SaveChanges();
                    int RecShipID = Context.tblReceivedShipments.AsQueryable().Select(x => x.id).OrderByDescending(x => x).FirstOrDefault();

                    List<tblShipmentDetail> objShipmentList = new List<tblShipmentDetail>();
                    for (int i = 0; i < receivedshipment.Description.Length; i++)
                    {
                        objShipmentList.Add(new tblShipmentDetail() { purchase_price = receivedshipment.PurchasePrice[i], quantity = receivedshipment.Quantity[i], comodity_code = receivedshipment.ComodityCode[i], description = receivedshipment.Description[i], Fk_shipment_id = RecShipID });
                    }
                    Context.BulkInsert(objShipmentList);
                    Context.SaveChanges();
                    Context.Configuration.ValidateOnSaveEnabled = false;
                }
                return SuccessMsg("Saved successfully");
            }
            catch (Exception ex)
            {
                return string.Format("'{0},false'", ex.Message.ToString());
            }
        }
        public List<dynamic> GetblReceivedShipmentById(int Id)
        {
            var lst = (from tblrec in Context.tblReceivedShipments join tblship in Context.tblShipmentDetails on tblrec.id equals tblship.Fk_shipment_id select new { tblrec.Fk_Customer_Id, tblrec.sender, tblrec.tracking, tblrec.carrier, tblrec.weight, tblrec.received_date, tblrec.height, tblrec.length, tblrec.created_on, tblrec.width, tblrec.box_condition, tblrec.staff_comments, tblrec.shelf_no, tblrec.status, tblrec.id, tblship.purchase_price, tblship.quantity, tblship.description, tblship.comodity_code,tblship.Fk_shipment_id }).Where(x=>x.Fk_shipment_id==Id).ToList<dynamic>();
            return lst.ToList();
        }
        int Temp;
        public List<ViewModelreceivedShipment> GetblReceivedShipment()
        {
            var Receivedlist = (from rec in Context.tblReceivedShipments join cus in Context.tblCustomers on rec.Fk_Customer_Id equals cus.Id orderby rec.created_on descending select new { cus.Id, cus.customer_reference,cus.email, rec.id, rec.received_date, rec.sender, rec.tracking, rec.carrier, rec.width, rec.height, rec.length, rec.weight, rec.box_condition, rec.staff_comments, rec.status, rec.shelf_no, rec.modified_on, rec.created_on, rec.is_requested_picture }).OrderByDescending(x=>x.created_on).ToList();
            List<ViewModelreceivedShipment> objtblReceivedShipment = new List<ViewModelreceivedShipment>();
            ViewModelreceivedShipment objmodel = new ViewModelreceivedShipment();
            foreach (var item in Receivedlist)
            {
                //int Checkstatus = item.status;
                //string stringValue = enumDisplayStatus.ToString();
                if (item.is_requested_picture == true)
                {
                    objmodel.chkimage = Context.tblShipmentImages.AsQueryable().Where(x => x.Fk_shipment_id == item.id).ToList<dynamic>();
                    Temp = 1;
                }
                else {
                    Temp = 0;
                }
                objtblReceivedShipment.Add(new ViewModelreceivedShipment() { status =Convert.ToString(item.status), id = item.id, Fk_assigned_user = item.email + "/"+item.customer_reference, received_date = item.received_date, sender = item.sender, tracking = item.tracking, carrier = item.carrier, weight = item.weight, height = item.height, length = item.length, width = item.width, box_condition = item.box_condition, staff_comments = item.staff_comments, created_on = item.created_on, modified_on = item.modified_on, shelf_no =Convert.ToString(item.shelf_no), is_requested_picture = item.is_requested_picture,chkimage = objmodel.chkimage, temp=Temp });
            }
            return objtblReceivedShipment.ToList();
        }
        public void DeletetblReceivedShipments(int Id)
        {
            var itemToRemove = Context.tblReceivedShipments.AsQueryable().SingleOrDefault(x => x.id == Id);
            if (itemToRemove != null)
            {
                Context.tblReceivedShipments.Remove(itemToRemove);
            }
            List<tblShipmentImage> itemToRemoves = Context.tblShipmentImages.AsQueryable().Where(x => x.Fk_shipment_id == Id).ToList();
            Context.tblShipmentImages.RemoveRange(itemToRemoves);
            Context.SaveChangesAsync();
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath(("~/Uploads/PackagesImages/") + Convert.ToString(Id)));
            if (di.Exists)
                di.Delete(true);
        }
        public void ChangeTblShipmentReturnStatus(int Id)
        {
            var itemToRemove = Context.tblShipmentReturnDetails.AsQueryable().SingleOrDefault(x => x.id == Id);
            if (itemToRemove != null)
            {
                var shipmentToRemove = Context.tblReceivedShipments.AsQueryable().SingleOrDefault(x => x.id == itemToRemove.Fk_shipment_id);
                if (shipmentToRemove!=null)
                {
                    shipmentToRemove.status = 9;
                    Context.Entry(shipmentToRemove).State = System.Data.Entity.EntityState.Modified;
                }
                Context.tblShipmentReturnDetails.Remove(itemToRemove);
            }
            Context.SaveChangesAsync();
        }
        public List<Repository.tblAdminUser> GettblAdminUser()
        {
            return Context.tblAdminUsers.OrderByDescending(x=>x.created_on).ToList();
        }
        public List<SelectListItem> Customer()
        {
            var stands = Context.tblCustomers.ToList();
            List<SelectListItem> objCustomrs = new List<SelectListItem>();
            objCustomrs.Add(new SelectListItem() { Text = "Select Customer", Value = "" });
            foreach (var item in stands)
            {
                objCustomrs.Add(new SelectListItem() { Value = Convert.ToString(item.Id), Text = item.customer_reference + " -- " + item.email });
            }

            return objCustomrs.ToList();
        }
        public List<tblShipmentDetail> GetTblShipmentDetailById(int Id)
        {

            return Context.tblShipmentDetails.AsQueryable().Where(x => x.Fk_shipment_id == Id).ToList();
        }
        public List<tblShipmentReturnDetail> GetTblShipmentReturnByFkId(int Id)
        {

            return Context.tblShipmentReturnDetails.AsQueryable().Where(x => x.Fk_shipment_id == Id).ToList();
        }
        public List<tblShipmentImage> GetTblShipmentImagesByfkId(int Id)
        {
            return Context.tblShipmentImages.AsQueryable().Where(x => x.Fk_shipment_id == Id).ToList();
        }
        public string InsertUpdateTblShipmentImages(int Fk_Id, HttpPostedFileBase[] input7)
        {
            try
            {
                if (input7 != null)
                {
                    string dirPath = HttpContext.Current.Server.MapPath("~/Uploads/PackagesImages/") + Convert.ToString(Fk_Id);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    List<tblShipmentImage> objship = new List<tblShipmentImage>();
                    foreach (HttpPostedFileBase item in input7)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        string file_path = dirPath + "\\" + fileName;
                        item.SaveAs(file_path);
                        objship.Add(new tblShipmentImage() { Fk_shipment_id = Fk_Id, file_path = fileName });
                    }
                    Context.tblShipmentImages.AddRange(objship);
                    Context.SaveChanges();
                }
                Context.SaveChanges();
                return SuccessMsg("Saved Successfully");
            }
            catch (Exception ex)
            {
                return string.Format("'{0},false'", ex.Message.ToString());
            }
        }
        public string DeleteTblShipmentImages(int Fk_Id, string file_path)
        {
            try
            {
                if (file_path != null)
                {
                    var itemToRemove = Context.tblShipmentImages.AsQueryable().SingleOrDefault(x => x.Fk_shipment_id == Fk_Id && x.file_path == file_path);
                    if (itemToRemove != null)
                    {
                        Context.tblShipmentImages.Remove(itemToRemove);
                        string fullPath = HttpContext.Current.Server.MapPath(("~/Uploads/PackagesImages/") + Convert.ToString(Fk_Id) + "/" + file_path);

                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }
                }
                Context.SaveChanges();
              
                return SuccessMsg("Deleted successfully");
            }
            catch (Exception ex)
            {
                return string.Format("'{0},false'", ex.Message.ToString());

            }
        }

    }
}