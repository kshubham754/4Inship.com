using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace _4InShip.com.Areas.User.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password  is required")]
        [ValidateCurrentPassword(ErrorMessage = "Old Password  is incorrect")]
        public string oldpassword { get; set; }
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password  is required")]
        public string newpassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Confirm Password  is required")]
        [System.ComponentModel.DataAnnotations.Compare("newpassword")]
        public string confirmpassword { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidateCurrentPasswordAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if ((new Areas.User.Services.SettingService()).CheckPassword(value.ToString()))
                    return ValidationResult.Success;
            }
            return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validatecurrentpassword"
            };

            yield return rule;
        }
    }
}