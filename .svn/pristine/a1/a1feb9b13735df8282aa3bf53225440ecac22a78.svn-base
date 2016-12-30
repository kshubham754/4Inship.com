using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Services
{
    public class CountryServices
    {
        Context4InshipEntities Context = new Context4InshipEntities();
       public List<SelectListItem>Country()
        {
            var  CountryList = Context.tblCountryMasters.ToList();
            var CtList = new List<SelectListItem>();
            CtList.Add(new SelectListItem() { Text = "Select Country",Value="" });
            foreach(var item in CountryList.ToList())
            {
                CtList.Add(new SelectListItem() { Text = item.country_name, Value = item.country_code });
            }
            return CtList.ToList();
        }
    }
}