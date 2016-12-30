using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(tbltblAdminUserproperties))]
    public partial class tblAdminUser
    {
    }
    public class tbltblAdminUserproperties
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [Display(Name ="Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Roll is required")]
        public string role { get; set; }
        public bool status { get; set; }
        public Nullable<System.DateTime> created_on { get; set; }
        public Nullable<System.DateTime> modified_on { get; set; }
    }
}