using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using web_api.Models;

namespace web_api.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {

        public PastDateAttribute() {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            NewItem item = (NewItem)validationContext.ObjectInstance;

            if (item.CreationDate > DateTime.Now)
            {
                return new ValidationResult("You can't create an item in the future");
            }

            return ValidationResult.Success;
        }
    }
}