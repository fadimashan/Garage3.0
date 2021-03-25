using Garage_G5.Migrations;
using Garage_G5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Validation
{
    public class CheckNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            const string errorMessage = "FirstName cannot be the same as the Lastname";
            if (value is string input)
            {
                var model = (Member)validationContext.ObjectInstance;
                if (model.LastName != input)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(errorMessage);
               
            }
            return new ValidationResult(errorMessage);
        }
    }
}
