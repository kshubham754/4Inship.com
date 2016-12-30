using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Repository;

namespace _4InShip.com.Areas.Admin.Controllers
{
    public class AjaxDatatablesPageingController : Controller
    {
        DataTableData objDatatable = new DataTableData();
        private List<ViewModelreceivedShipment> _data = new ReceivedShipmentService().GetblReceivedShipment();
        private int TOTAL_ROWS = new ReceivedShipmentService().GetblReceivedShipment().Count;
        // GET: Admin/AjaxDatatablesPageing
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjaxGetJsonData(int draw, int start, int length=5)
        {
            string search = Request.QueryString["search[value]"];
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            if (Request.QueryString["order[0][column]"] != null)
            {
                sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
            }
            if (Request.QueryString["order[0][dir]"] != null)
            {
                sortDirection = Request.QueryString["order[0][dir]"];
            }

            DataTableData dataTableData = new DataTableData();
            dataTableData.draw = draw;
            dataTableData.recordsTotal = TOTAL_ROWS;
            int recordsFiltered = 0;
            dataTableData.data = FilterData(ref recordsFiltered, start, length, search, sortColumn, sortDirection);
            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }
        private List<ViewModelreceivedShipment> FilterData(ref int recordFiltered, int start, int length, string search, int sortColumn, string sortDirection)
        {

            List<ViewModelreceivedShipment> list = new List<ViewModelreceivedShipment>();
            if (search == null)
            {
                list = _data;
            }
            else
            {
                // simulate search
                foreach (ViewModelreceivedShipment dataItem in _data)
                {
                    if (dataItem.carrier.ToUpper().Contains(search.ToUpper()) ||
                        dataItem.id.ToString().Contains(search.ToUpper())) 
                    {
                        list.Add(dataItem);
                    }
                }
            }

            // simulate sort
            if (sortColumn == 0)
            {// sort Name
                list.Sort((x, y) => objDatatable.SortString(x.carrier, y.carrier, sortDirection));
            }
            
            

            recordFiltered = list.Count;

            // get just one page of data
            list = list.GetRange(start, Math.Min(length, list.Count - start));

            return list;
        }
    }
    public class DataTableData
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ViewModelreceivedShipment> data { get; set; }

        public int SortString(string s1, string s2, string sortDirection)
        {
            return sortDirection == "asc" ? s1.CompareTo(s2) : s2.CompareTo(s1);
        }

        public int SortInteger(string s1, string s2, string sortDirection)
        {
            int i1 = int.Parse(s1);
            int i2 = int.Parse(s2);
            return sortDirection == "asc" ? i1.CompareTo(i2) : i2.CompareTo(i1);
        }

        public int SortDateTime(string s1, string s2, string sortDirection)
        {
            DateTime d1 = DateTime.Parse(s1);
            DateTime d2 = DateTime.Parse(s2);
            return sortDirection == "asc" ? d1.CompareTo(d2) : d2.CompareTo(d1);
        }


    }


}