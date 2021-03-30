using Garage_G5.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [CheckName]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipType MembershipType { get; set; }
        public string Phone { get; set; }

        //[DisplayName("Date of Birth")]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "YYYY/mm/dd", ApplyFormatInEditMode =true)]
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
       [Required]
       [Remote("IsCodeNumberExists", "Members", ErrorMessage = "Personal Code Number exists already!" , AdditionalFields = "Id")]
       [SwedishPersonalNumber]
        public string PersonalIdNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime BonusAccountExpires { get; set; }
        public bool IsGolden { get; set; }

        public string FullName { get => FirstName + " " + LastName; }

        public List<ParkedVehicle> MemberVehicles { get; set; }

        public bool IsUnderage { get; set; }

    }
}
