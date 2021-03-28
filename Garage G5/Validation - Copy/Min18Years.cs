using Garage_G5.Migrations;
using Garage_G5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Validation
{
    public class Min18Years :ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var member = (Member)validationContext.ObjectInstance;

            if (member.DateOfBirth == null)
                return new ValidationResult("Date of Birth is required.");

            var age = DateTime.Today.Year - member.DateOfBirth.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Member should be at least 18 years old.");
        }
    }
}

