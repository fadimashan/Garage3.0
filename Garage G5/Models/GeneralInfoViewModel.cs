using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    [Keyless]
    public class GeneralInfoViewModel
    {

        //private DateTime? parkedTime;
        //public int Id { get; set; }
        //public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
        //public IEnumerable<SelectListItem> Types { get; set; }
        //public VehicleType? VehicleType { get; set; }
        //public TimeSpan TotalParkedTime { get; set; }
        //public string RegistrationNum { get; set; }
        //public DateTime EnteringTime
        //{
        //    get { return parkedTime ?? DateTime.Now; }
        //    set { parkedTime = value; }
        //}

        private DateTime? dateCreated;

        public int Id { get; set; }
        public VehicleType? VehicleType { get; set; }
        public string RegistrationNum { get; set; }

        public DateTime EnteringTime
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        public TimeSpan TotalTimeParked { get; set; }



    }
}
