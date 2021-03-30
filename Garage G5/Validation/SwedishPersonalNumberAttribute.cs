﻿using Garage_G5.Models;
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

                //var correctDoB = owner.DateOfBirth.ToString("yyyy") + owner.DateOfBirth.ToString("MM") + owner.DateOfBirth.ToString("dd");
                //var firstEight = owner.PersonalIdNumber.Substring(0,8);

                
                //if (correctDoB == firstEight)
                //{
                    
                //}
                var dateOfB = (owner.DateOfBirth.ToString("yyyy")) + (owner.DateOfBirth.ToString("MM")) + (owner.DateOfBirth.ToString("dd"));

                //var dateOfB = owner.DateOfBirth.ToString().Substring(0,10);
                //dateOfB.Split("-");
                string [] subs = dateOfB.Split('-');
                StringBuilder builder = new StringBuilder();
                foreach (string part in subs)
                {
                    builder.Append(part);
                }
                string resultOfBdd = builder.ToString();
                //dateOfB.ToString();
                // dateOfB.Substring(0,8);
               // var person = owner.PersonalIdNumber.ToString();
             
                var fourDigits = input.Substring(8);
                if (int.TryParse(fourDigits,out int res))
                {
                var personNumer    = string.Concat(resultOfBdd, res);
                    //var personNumer = dateOfB+res;
                    var pesNum = owner.PersonalIdNumber;
                    if (pesNum == personNumer)
                    {
                        return ValidationResult.Success;

                    }
                    else return new ValidationResult("Personal code number does not match with the date of birth");

                }
                return new ValidationResult("Please check the personalcode format");


            }
            return new ValidationResult("Please check the personalcode format");
        }
    }
    
}
