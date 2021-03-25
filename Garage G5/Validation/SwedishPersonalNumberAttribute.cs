using Garage_G5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Validation
{
    public class SwedishPersonalNumberAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
             if(value is string input && input.Length==8)
             {
                var owner = (Member)validationContext.ObjectInstance;
               
              var dateOfB = owner.DateOfBirth.ToString().Substring(0,4);
                var person = owner.PersonalIdNumber.ToString();
             

                var fourDigits = input.Substring(4);
                if (int.TryParse(fourDigits,out int res))
                {
              var personNumer    = string.Concat(dateOfB ,res);
                    //var personNumer = dateOfB+res;
                    var pesNum = owner.PersonalIdNumber;
                    if (pesNum == personNumer)
                    {
                        return ValidationResult.Success;

                    }
                    else return new ValidationResult("The format is wrong");

                }
                return new ValidationResult("The format is wrong");


            }
            return new ValidationResult("The format is wrong");
        }
    }
    
}
