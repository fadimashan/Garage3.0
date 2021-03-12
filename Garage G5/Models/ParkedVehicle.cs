using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage_G5.Models
{

    public class ParkedVehicle
    {
        private DateTime? dateCreated;

        public int Id { get; set; }
        [Display(Name = "Type")]
        public VehicleType VehicleType { get; set; }

        [Display(Name = "Reg")]
        [Remote("IsRegisterNumberExists", "ParkedVehicles", ErrorMessage = "Registration Number already in use")]
        public string RegistrationNum { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        [Display(Name = "Wheels")]
        public int WheelsNum { get; set; }

        [Display(Name = "Parked")]
        public DateTime EnteringTime { get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }

    }
}
