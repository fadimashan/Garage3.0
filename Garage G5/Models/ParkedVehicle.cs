using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage_G5.Models
{
    public class ParkedVehicle
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Type")]
        public VehicleType? VehicleType { get; set; }
        [Display(Name = "Registration")]
        [Required]
        [Remote("IsRegisterNumberExists", "ParkedVehicles", ErrorMessage = "Registration Number already in use", AdditionalFields = "Id")]
        [StringLength(10, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string RegistrationNum { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string Color { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string Brand { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Model { get; set; }
        [Required]
        [Display(Name = "Wheels")]
        [Range(2,12)]
        public int WheelsNum { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnteringTime { get;set; }
        [NotMapped]
        public IEnumerable<SelectListItem> GetVehiclesType { get; set; }

    }
}
