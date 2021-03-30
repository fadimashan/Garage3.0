using Garage_G5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_G5.Validation
{
    public class SwedishPersonalNumberAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
             if(value is string input && input.Length==12)
             {
                var owner = (Member)validationContext.ObjectInstance;
                var dateOfB = (owner.DateOfBirth.ToString("yyyy"))+(owner.DateOfBirth.ToString("MM"))+ (owner.DateOfBirth.ToString("dd"));
                string [] subs = dateOfB.Split('-');
                StringBuilder builder = new StringBuilder();
                foreach (string part in subs)
                {
                    builder.Append(part);
                }
                string resultOfBdd = builder.ToString();             
                var fourDigits = input.Substring(8);
                if (int.TryParse(fourDigits,out int res))
                {
                var personNumer = string.Concat(resultOfBdd, res);
                    var pesNum = owner.PersonalIdNumber;
                    if (pesNum == personNumer)
                    {
                        return ValidationResult.Success;

                    }
                    else return new ValidationResult("Please check the personalcode format");

                }
                return new ValidationResult("Please check the personalcode format");


            }
            return new ValidationResult("Please check the personalcode format");
        }
    }
    
}
