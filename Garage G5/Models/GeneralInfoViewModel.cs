using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    public class GeneralInfoViewModel
    {

        private DateTime? parkedTime;
        public int Id { get; set; }
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        [Display(Name = "Type")]
        public VehicleType? VehicleType { get; set; }
        [Display(Name = "Time parked")]
        public TimeSpan TotalParkedTime { get; set; }
        [Display(Name = "Registration")]
        public string RegistrationNum { get; set; }
        [Display(Name = "Arrival")]
        public DateTime EnteringTime
        {
            get { return parkedTime ?? DateTime.Now; }
            set { parkedTime = value; }
        }

    }
}
