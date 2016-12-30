using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _4InShip.com.Repository
{
    [MetadataType(typeof(PlanProperties))]
    public partial class tblPlan
    {
       
    }
    public class PlanProperties
    {
        [Required(ErrorMessage = "Title is required")]
        public string title { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public string price { get; set; }
        public string recurring_duration { get; set; }
        public string status { get; set; }
        public string is_recurring { get; set; }
        public DateTime created_on { get; set; }
        public DateTime modified_on { get; set; }
        [Required(ErrorMessage = "Free Storage Days is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Valid Integer Number")]
        public int free_storage_days { get; set; }
    }
        //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
        //public class MustBeTrueAttribute : ValidationAttribute, IClientValidatable
        //{
        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        if ((bool)value)
        //            return ValidationResult.Success;
        //        return new ValidationResult(String.Format(ErrorMessageString, validationContext.DisplayName));
        //    }

        //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //    {
        //        var rule = new ModelClientValidationRule
        //        {
        //            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
        //            ValidationType = "checkrequired"
        //        };

        //        yield return rule;
        //    }
        //}
    
}