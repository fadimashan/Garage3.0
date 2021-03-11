using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    public class GeneralInfoViewModel
    {
        public int Id { get; set; }
        private DateTime? dateCreated;
        public VehicleType VehicleType { get; set; }
        //public DateTime TotalParkedTime { get; set; }
        public string RegistrationNum { get; set; }
        public DateTime EnteringTime
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        public TimeSpan TotalTimeParked { get; set; }
        //public DateTime TotalTimeParked()
        //{
        //    return EnteringTime - CurrentTime;
        //}

    }
}
