using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4InShip.com.Repository;
using Microsoft.Practices.Unity;
using _4InShip.com.Areas.User.Models;
using _4InShip.com.Areas.User.IServices;
using System.Data.Entity;
using EntityFramework.BulkInsert.Extensions;

namespace _4InShip.com.Areas.User.Services
{
    public class ShipmentsService : _4InShip.com.Areas.Admin.Models.clsCommon, IShipmentService<tblReceivedShipment, tblOrder, UserViewCustomerModel, UserPaymentModel, ViewRecivedShipmentModel, tblAddressBook, ViewPackagingOptionsModel, ViewOrderModel, ShipmentMethodPackage, PacakageOptionArray>
    {

        [Dependency]
        public Context4InshipEntities Context { get; set; }

        public List<tblReceivedShipment> GetRecievedShipment(int ID)
        {
            try
            {

                List<tblReceivedShipment> TblRecivedShipment = Context.tblReceivedShipments.OrderByDescending(x => x.id).Where(x => x.Fk_Customer_Id == ID).ToList();
                return TblRecivedShipment.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tblOrder> GetOrderRecordList(int ID)
        {
            try
            {
                List<tblOrder> OrderList = Context.tblOrders.OrderByDescending(x => x.id).Take(3).Where(x => x.Fk_customer_id == ID).ToList();
                return OrderList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserViewCustomerModel GetCustomerPlans(int ID)
        {
            try
            {
                UserViewCustomerModel objUserViewCustomerModel = new UserViewCustomerModel();
                var GetCutomerPlanRecord = Context.tblCustomerPlanLinkings.Where(x => x.Fk_Customer_Id == ID).Select(x => x).FirstOrDefault();
                var GetAddresbookRecord = Context.tblAddressBooks.Where(x => x.Fk_customer_Id == ID).Select(x => x).FirstOrDefault();
                var GetCustomerRecord = Context.tblCustomers.Where(x => x.Id == ID).Select(x => x).FirstOrDefault();
                decimal GetPansPrice = Convert.ToDecimal(Context.tblPlans.Max(x => x.price));
                if (GetPansPrice > GetCutomerPlanRecord.plan_amount)
                {
                    objUserViewCustomerModel.PlanPrice = GetPansPrice;
                }
                objUserViewCustomerModel.PlanTitle = GetCutomerPlanRecord.plan_title;
                objUserViewCustomerModel.Email = GetCustomerRecord.email;
                objUserViewCustomerModel.FirstName = GetAddresbookRecord.first_name;
                objUserViewCustomerModel.LastName = GetAddresbookRecord.last_name;
                return objUserViewCustomerModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        int Temp = 0;
        public int yourpoints()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                var pointsList = Context.tblRewardPoints.AsQueryable().Where(x => x.is_converted == false && x.Fk_CustomerId == id).ToList();
                if (pointsList != null)
                {
                    foreach (var item in pointsList)
                    {
                        Temp = Convert.ToInt32(Temp + item.reward_points);
                    }
                }
                return Temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int Temp1 = 0, Temp2 = 0, balance = 0;
        public int yourBalance()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                var balanceList = Context.tblRewardAmounts.AsQueryable().Where(x => x.Fk_CustomerId == id).ToList();
                if (balanceList != null)
                {
                    foreach (var item in balanceList)
                    {
                        Temp1 = Convert.ToInt32(Temp1 + item.credit);
                        Temp2 = Convert.ToInt32(Temp2 + item.debit);
                    }
                    balance = Temp1 - Temp2;
                }
                return balance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserPaymentModel> GetCustomerPayment(int ID)
        {
            List<UserPaymentModel> objiUserPaymentModel = new List<UserPaymentModel>();
            var PaymentList = Context.tblInvoices.Where(x => x.Fk_customer_id == ID).Take(5).ToList();
            foreach (var item in PaymentList)
            {
                objiUserPaymentModel.Add(new UserPaymentModel() { PaymentDate = item.invoice_date, Method = item.payment_method, OrderReferenceNumber = item.reference_no, Amount = item.payment_amount, Status = item.paid_status });
            }
            return objiUserPaymentModel.ToList();
        }

        public List<ViewRecivedShipmentModel> GetReciveShipmentDetails(int ID)
        {
            try
            {
                List<ViewRecivedShipmentModel> objReceivedshiment = new List<ViewRecivedShipmentModel>();
                ViewRecivedShipmentModel objrecshipment = new ViewRecivedShipmentModel();
                var Model = Context.tblReceivedShipments.Where(x => x.Fk_Customer_Id == ID).ToList();
                foreach (var item in Model)
                {
                    int TotalDays = 0;
                    var GetPlansDays = Context.tblCustomerPlanLinkings.Where(x => x.Fk_Customer_Id == ID).Select(x => x).FirstOrDefault();
                    objrecshipment.RecivedShipment_ImagePath = Context.tblShipmentImages.Where(x => x.Fk_shipment_id == item.id).Select(x => x.file_path).ToList<dynamic>();
                    if (GetPlansDays != null)
                    {
                        int TotalstorageDays = Convert.ToInt32((DateTime.Now.Date - item.received_date.Date).TotalDays);
                        TotalDays = Convert.ToInt32(GetPlansDays.free_storage_days) - TotalstorageDays;
                    }
                    objReceivedshiment.Add(new ViewRecivedShipmentModel() { sender = item.sender, received_date = item.received_date, length = item.length, weight = item.weight, width = item.width, height = item.height, box_condition = item.box_condition, staff_comments = item.staff_comments, status = item.status, tracking = item.tracking, ID = item.id, StorageDays = TotalDays, RecivedShipment_ImagePath = objrecshipment.RecivedShipment_ImagePath });
                }
                return objReceivedshiment.ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<ViewRecivedShipmentModel> GetShipmentDetails()
        {
            try
            {
                List<ViewRecivedShipmentModel> objReceivedshiment = new List<ViewRecivedShipmentModel>();
                var ShipmentDetails = (from tblrec in Context.tblReceivedShipments join tblship in Context.tblShipmentDetails on tblrec.id equals tblship.Fk_shipment_id select new { tblship.description, tblship.purchase_price, tblship.quantity, tblship.Fk_shipment_id }).ToList();
                foreach (var item in ShipmentDetails)
                {
                    objReceivedshiment.Add(new ViewRecivedShipmentModel() { purchase_price = item.purchase_price, quantity = item.quantity, description = item.description, Fk_shipment_id = item.Fk_shipment_id });
                }
                return objReceivedshiment.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblAddressBook GettblAdressData(int ID)
        {
            try
            {
                tblAddressBook objAddress = new tblAddressBook();
                objAddress.Id = ID;
                tblAddressBook tblobjAddress = Context.tblAddressBooks.Where(x => x.Id == objAddress.Id && x.is_default == true).FirstOrDefault();
                var tblAddressbooktbldata = (from tbladdress in Context.tblAddressBooks join tblcontry in Context.tblCountryMasters on tbladdress.country_code equals tblcontry.country_code select new { tblcontry.country_name, tbladdress.country_code }).Where(x => x.country_code == tblobjAddress.country_code).FirstOrDefault();
                tblobjAddress.country_code = tblAddressbooktbldata.country_name;
                return tblobjAddress;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ViewPackagingOptionsModel> GetoptionplanPlandata(int ID)
        {
            List<ViewPackagingOptionsModel> objViewPackagingOptionsModel = new List<ViewPackagingOptionsModel>();
            var PLanlinkList = (from plan in Context.tblPlans join customerplan in Context.tblCustomerPlanLinkings on plan.title equals customerplan.plan_title join planlink in Context.tblPlanOptionLinkings on plan.Id equals planlink.Fk_plan_id join planoption in Context.tblPackagingOptions on planlink.Fk_packaging_option_id equals planoption.Id select new { customerplan.Id, planoption.title, planoption.is_shipment, planoption.description, plantitle = plan.title, planlink.price }).Where(x => x.is_shipment == true && x.Id == ID).ToList();
            foreach (var item in PLanlinkList)
            {
                objViewPackagingOptionsModel.Add(new ViewPackagingOptionsModel { title = item.title, price = item.price, description = item.description });
            }
            return objViewPackagingOptionsModel.ToList();
        }
        public string CancelRecShipment(int ID)
        {
            try
            {
                tblReceivedShipment objRec = new tblReceivedShipment();
                objRec.id = ID;
                tblReceivedShipment GetRecord = Context.tblReceivedShipments.Where(x => x.id == objRec.id).Select(x => x).FirstOrDefault();
                GetRecord.status = 7;
                Context.Entry(GetRecord).State = EntityState.Modified;
                Context.SaveChanges();
                return "Shipment  has been  canceled successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetRequestPicture(int ID)
        {
            try
            {
                tblReceivedShipment objRec = new tblReceivedShipment();
                objRec.id = ID;
                tblReceivedShipment GetRecord = Context.tblReceivedShipments.Where(x => x.id == objRec.id).Select(x => x).FirstOrDefault();
                if (GetRecord != null)
                {
                    GetRecord.is_requested_picture = true;
                    Context.Entry(GetRecord).State = EntityState.Modified;
                    Context.SaveChanges();
                }
                return "Request for shipments picture  has been  submited successfully ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetReturnPanding(int ID)
        {
            try
            {
                tblReceivedShipment objRec = new tblReceivedShipment();
                objRec.id = ID;
                tblReceivedShipment GetRecord = Context.tblReceivedShipments.Where(x => x.id == objRec.id).Select(x => x).FirstOrDefault();
                if (GetRecord != null)
                {
                    GetRecord.status = 8;
                    Context.Entry(GetRecord).State = EntityState.Modified;
                    Context.SaveChanges();
                }
                return "Request for return packages  has been  submited  successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SaveInvoicefilepath(tblReceivedShipment objrecivedship)
        {
            try
            {
                var SaveFilePath = Context.tblReceivedShipments.Where(x => x.id == objrecivedship.id).Select(x => x).FirstOrDefault();
                if (SaveFilePath != null)
                {
                    SaveFilePath.created_invoice_file_path = objrecivedship.created_invoice_file_path;
                    Context.Entry(SaveFilePath).State = EntityState.Modified;
                    Context.SaveChanges();

                }
                return "Invoice has been uploaded submited  successfully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReturnPackageData(ViewRecivedShipmentModel objViewRecivedShipmentModel)
        {
            try
            {
                tblShipmentReturnDetail shipdetail = Context.tblShipmentReturnDetails.Where(x => x.Fk_shipment_id == objViewRecivedShipmentModel.Fk_shipment_id).Select(x => x).FirstOrDefault();
                tblShipmentReturnDetail objtblShipmentReturnDetail = new tblShipmentReturnDetail();
                if (shipdetail == null)
                {
                    if (objViewRecivedShipmentModel.ReturnpackagefileUpload != null)
                    {
                        objtblShipmentReturnDetail.Fk_shipment_id = objViewRecivedShipmentModel.Fk_shipment_id;
                        objtblShipmentReturnDetail.prepaid_label_filename = objViewRecivedShipmentModel.ReturnpackagefileUpload;
                        Context.tblShipmentReturnDetails.Add(objtblShipmentReturnDetail);
                        Context.SaveChanges();
                    }
                    else
                    {
                        objtblShipmentReturnDetail.Fk_shipment_id = objViewRecivedShipmentModel.Fk_shipment_id;
                        objtblShipmentReturnDetail.first_name = objViewRecivedShipmentModel.FirstName;
                        objtblShipmentReturnDetail.last_name = objViewRecivedShipmentModel.LastName;
                        objtblShipmentReturnDetail.address1 = objViewRecivedShipmentModel.Address1;
                        objtblShipmentReturnDetail.address2 = objViewRecivedShipmentModel.Address1;
                        objtblShipmentReturnDetail.city = objViewRecivedShipmentModel.Address2;
                        objtblShipmentReturnDetail.state = objViewRecivedShipmentModel.State;
                        objtblShipmentReturnDetail.postal_code = objViewRecivedShipmentModel.Zip;
                        objtblShipmentReturnDetail.mobile = objViewRecivedShipmentModel.Phone;
                        objtblShipmentReturnDetail.rma = objViewRecivedShipmentModel.Phone;
                        Context.tblShipmentReturnDetails.Add(objtblShipmentReturnDetail);
                        Context.SaveChanges();
                    }
                    return "Return  has been  packages submited  successfully ";
                }
                else
                {
                    if (objViewRecivedShipmentModel.ReturnpackagefileUpload != null)
                    {
                        shipdetail.Fk_shipment_id = objViewRecivedShipmentModel.Fk_shipment_id;
                        shipdetail.prepaid_label_filename = objViewRecivedShipmentModel.ReturnpackagefileUpload;
                        Context.Entry(shipdetail).State = EntityState.Modified;
                        Context.SaveChanges();
                    }
                    else
                    {
                        objtblShipmentReturnDetail.Fk_shipment_id = objViewRecivedShipmentModel.Fk_shipment_id;
                        shipdetail.first_name = objViewRecivedShipmentModel.FirstName;
                        shipdetail.last_name = objViewRecivedShipmentModel.LastName;
                        shipdetail.mobile = objViewRecivedShipmentModel.Phone;
                        shipdetail.address1 = objViewRecivedShipmentModel.Address1;
                        shipdetail.address2 = objViewRecivedShipmentModel.Address2;
                        shipdetail.city = objViewRecivedShipmentModel.City;
                        shipdetail.state = objViewRecivedShipmentModel.State;
                        shipdetail.postal_code = objViewRecivedShipmentModel.Zip;
                        shipdetail.mobile = objViewRecivedShipmentModel.Phone;
                        shipdetail.rma = objViewRecivedShipmentModel.Rma;
                        Context.Entry(shipdetail).State = EntityState.Modified;
                        Context.SaveChanges();
                    }
                    return "Return  has been  packages submited  successfully";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CreateShipmentOrder(List<ShipmentMethodPackage> objShipmentMethodPackage, List<PacakageOptionArray> objPacakageOptionArray, ViewOrderModel objViewModelOrder)
        {
            try
            {
                string[] RecivedshipmentID = objViewModelOrder.ShipmentCheckboxID;
                if (RecivedshipmentID.Length == 0)
                {
                    return "";
                }
                else
                {
                    //Add Data RecivedShipmentdata//
                    for (int i = 0; i < RecivedshipmentID.Length; i++)
                    {
                        if (RecivedshipmentID[i] != "")
                        {
                            int id = Convert.ToInt32(RecivedshipmentID[i].Replace("id", ""));
                            RecivedCheckboxid(objViewModelOrder.Fk_Customer_id, id);
                        }
                    }
                    //Use function Add Data TblDeliverAddress
                    ShipmentDeliverdAddress(objViewModelOrder.Fk_Customer_id, objViewModelOrder.Fk_orderAddres_id);
                    //Use function  Add Data tblOrder
                    CreateOrder();

                    //Use function  Add Data tblorderCahrges
                    OrderCharges(objShipmentMethodPackage, objPacakageOptionArray, objViewModelOrder);
                    return "";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ShipmentDeliverdAddress(int fkcustomerid, int fkorderaddressid)
        {
            var GetAddressBookData = Context.tblAddressBooks.Where(x => x.Fk_customer_Id == fkcustomerid && x.Id == fkorderaddressid).Select(x => x).FirstOrDefault();
            if (GetAddressBookData != null)
            {
                tblDeliveryAddress objtblDeliveryAddress = new tblDeliveryAddress();
                objtblDeliveryAddress.address1 = GetAddressBookData.address1;
                objtblDeliveryAddress.address2 = GetAddressBookData.address2;
                objtblDeliveryAddress.city = GetAddressBookData.city;
                objtblDeliveryAddress.company = GetAddressBookData.company;
                objtblDeliveryAddress.country_code = GetAddressBookData.country_code;
                objtblDeliveryAddress.first_name = GetAddressBookData.first_name;
                objtblDeliveryAddress.last_name = GetAddressBookData.last_name;
                objtblDeliveryAddress.postal_code = GetAddressBookData.post_code;
                objtblDeliveryAddress.state = GetAddressBookData.state;
                objtblDeliveryAddress.mobile1 = GetAddressBookData.mobile1;
                objtblDeliveryAddress.mobile2 = GetAddressBookData.mobile2;
                Context.tblDeliveryAddresses.Add(objtblDeliveryAddress);
                Context.SaveChanges();
            }
        }
        public void OrderCharges(List<ShipmentMethodPackage> objShipmentMethodPackage, List<PacakageOptionArray> objPackagingOptions, ViewOrderModel objVieworderModel)
        {
            ViewOrderModel objViewOrderModel = new ViewOrderModel();
            List<tblOrderCharge> tblOrderChargeList = new List<tblOrderCharge>();
            foreach (var item in objShipmentMethodPackage)
            {
                tblOrderChargeList.Add(new tblOrderCharge() { charge_name = item.shipmentdescription, sell_price = Convert.ToDecimal(item.shipmentMethodPrice) });

            }
            Context.BulkInsert(tblOrderChargeList);
            foreach (var item in objPackagingOptions)
            {
                tblOrderChargeList.Add(new tblOrderCharge() { charge_name = item.PackageOptionTitle, sell_price = Convert.ToDecimal(item.PackageOptionPrice) });
            }
            Context.BulkInsert(tblOrderChargeList);
        }
        public void CreateOrder()
        {
            int GetDeliverAddressbookID = Context.tblDeliveryAddresses.OrderByDescending(x => x.id).Select(x => x.id).FirstOrDefault();
            ViewOrderModel objViewOrderModel = new ViewOrderModel();
            tblOrder objtblOrder = new tblOrder();
            objtblOrder.Fk_delivery_address_id = GetDeliverAddressbookID;
            objtblOrder.Fk_invoice_id = 0;
            objtblOrder.reference_no = "123";
            objtblOrder.carrier = "DHL";
            objtblOrder.product = "shipmentOrder";
            objtblOrder.service = "OrderServices";
            objtblOrder.pickup_date = DateTime.Now;
            objtblOrder.pickup_cut_off_time = DateTime.Now;
            objtblOrder.booking_time = DateTime.Now;
            objtblOrder.billing_weight = 25;
            objtblOrder.is_delivered = true;
            objtblOrder.signature = "Customer";
            objtblOrder.status = 1;
            objtblOrder.Fk_customer_id = objViewOrderModel.Fk_Customer_id;
            Context.tblOrders.Add(objtblOrder);
            Context.SaveChanges();
        }
        public void RecivedCheckboxid(int CustomerID, int id)
        {
            var RecivedShipmentData = Context.tblReceivedShipments.Where(x => x.Fk_Customer_Id == CustomerID && x.id == id).Select(x => x).FirstOrDefault();
            if (RecivedShipmentData != null)
            {
                RecivedShipmentData.Fk_order_id = 1;
                RecivedShipmentData.status = 10;
                Context.Entry(RecivedShipmentData).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }


    }
}