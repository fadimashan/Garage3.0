using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    public class GeneralInfoViewModel
    {
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
        public IEnumerable<SelectListItem>  Types { get; set; }
        public VehicleType? VehicleType { get; set; }
        private DateTime? parkedTime;
        public TimeSpan TotalParkedTime { get; set; }
        public string RegistrationNum { get; set; }
        public DateTime EnteringTime
        {
            get { return parkedTime ?? DateTime.Now; }
            set { parkedTime = value; }
        }
       

        //public DateTime TotalTimeParked()
        //{
        //    return EnteringTime - CurrentTime;
        //}

    }
}
