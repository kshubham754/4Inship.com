using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4InShip.com.Areas.Admin.Models
{

    public  enum enumReceivShipmentStatus
    {
       
        Ready=1,
        Processing=2,
        Payment_Awaiting=3,
        On_Hold=4,
        Prohibitted=5,
        Dispossed_Off = 7,
        Return_Pending = 8,
        Returned = 9,
        Order_Created=10
       


    }
    public class enumsecond
    {
        private enumReceivShipmentStatus enumclass;

        public void Setenum (enumReceivShipmentStatus value)
        {
            enumclass = value;
        }

        public enumReceivShipmentStatus Getenum()
        {
            return enumclass;
        }
       
    }
    public enum enumorder
    {

        Awaiting_Confirmation = 1,
        In_Queue = 2,
        Proccessing=3,
        Awaiting_Payment = 4,
        Confirmed = 5,
        Canceled = 6,
        In_Transit = 7,
        Delivered = 8


    }
    public class enumseconds
    {
        private enumorder enumclass;

        public void Setenum(enumorder value)
        {
            enumclass = value;
        }

        public enumorder Getenum()
        {
            return enumclass;
        }

    }


}