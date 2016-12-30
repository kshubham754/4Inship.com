using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(tblAddressBookproperties))]
    public partial class tblAddressBook
    {

    }
    public class tblAddressBookproperties
    {
        public int Id { get; set; }

        public int Fk_customer_Id { get; set; }

        [Required(ErrorMessage = "Address Name is required")]
        [ValidateAddressName(ErrorMessage ="Address Name already exists.")]
        [Display(Name ="Address Name")]
        public string address_name { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string last_name { get; set; }
        public string company { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address1 { get; set; }
        public string address2 { get; set; }

        public string country_code { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string state { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "Postal Code is required")]
        public string post_code { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        public string mobile1 { get; set; }
        public string mobile2 { get; set; }
        public string tax_id { get; set; }
        public bool is_default { get; set; }
        public System.DateTime created_on { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidateAddressNameAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if ((new Areas.User.Services.SettingService()).CheckDefaultAddress(value.ToString(), HttpContext.Current.Request.RawUrl.Split('/').Last()))
                    return ValidationResult.Success;
            }
            return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validateaddressname"
            };

            yield return rule;
        }
    }
}