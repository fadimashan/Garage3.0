using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    [Keyless]
    public class GeneralInfoViewModel
    {
        public int Id { get; set; }
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public string VehicleType { get; set; }
        [Display(Name = "Time in garage")]
        public TimeSpan TotalParkedTime { get; set; }
        [Display(Name = "Registration")]
        public string RegistrationNum { get; set; }
        [Display(Name = "Arrival")]
        public DateTime EnteringTime
        {
            get; set;
        }
        [Display(Name = "Parked")]
        public TimeSpan TotalTimeParked { get => DateTime.Now - EnteringTime;  }
        public string  Brand { get; }
        public bool? IsInGarage { get; set; }


    }
}
