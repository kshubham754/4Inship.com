﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Areas.Admin.Models
{
    public class clsCommon
    {
        public string SuccessMsg(string msg)
        {
            return string.Format("showMessage('{0}',true);", msg);
        }
        public string ErrorMsg(string msg)
        {
            return string.Format("showMessage('{0}',false);", msg);
        }
        public string Status(string Statustype)
        {
            if (Statustype == "1")
            {
                Statustype = "label-success" + "," + "Ready";

            }
            else if (Statustype == "2")
            {
                Statustype = "label-primary" + "," + "Processing";

            }
            else if (Statustype == "3")
            {
                Statustype = "label-default" + "," + "Payment Awaiting";

            }
            else if (Statustype == "4")
            {
                Statustype = "label-warning" + "," + "On_Hold";

            }
            else if (Statustype == "5")
            {
                Statustype = "label-danger" + "," + "Prohibitted";

            }
            else if (Statustype == "6")
            {
                Statustype = "label-danger" + "," + "Dispossed_Off";

            }
            else if (Statustype == "7")
            {
                Statustype = "label-warning" + "," + "Return_Pending";

            }
            else if (Statustype == "8")
            {
                Statustype = "label-success" + "," + "Returned";

            }
            else if(Statustype=="10")
            {
                Statustype = "label-warning" + "," + "Order_Created";
            }

            return Statustype;

        }
        public string OrderStatus(string Statustype)
        {
            if (Statustype == "1")
            {
                Statustype = "label-danger" + "," + "Awaiting_Confirmation";

            }
            else if (Statustype == "2")
            {
                Statustype = "label-primary" + "," + "In_Queue";

            }
            else if (Statustype == "3")
            {
                Statustype = "label-default" + "," + "Awaiting_Payment";

            }
            else if (Statustype == "4")
            {
                Statustype = "label-success" + "," + "Confirmed";

            }
            else if (Statustype == "5")
            {
                Statustype = "label-danger" + "," + "Canceled";

            }
            else if (Statustype == "6")
            {
                Statustype = "label-danger" + "," + "In_Transit";

            }
            else if (Statustype == "7")
            {
                Statustype = "label-success" + "," + "Delivered";

            }
            

            return Statustype;

        }
    }



}