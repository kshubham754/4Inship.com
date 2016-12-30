using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(tblpackagingoptionsproperties))]
    public partial class tblPackagingOption
    {
    }
    public class tblpackagingoptionsproperties
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [System.Web.Mvc.AllowHtml]
        public string description { get; set; }
        public bool status { get; set; }

        public DateTime created_on { get; set; }

        public DateTime modified_on { get; set; }
    }
}