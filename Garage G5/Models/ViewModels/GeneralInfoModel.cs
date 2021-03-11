using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    public class GeneralInfoModel
    {
        private DateTime? dateCreated;
        public VehicleType VehicleType { get; }
        //public DateTime TotalParkedTime { get; set; }
        public string RegistrationNum { get; }
        public DateTime EnteringTime
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }

        //public DateTime TotalTimeParked()
        //{
        //    return EnteringTime - CurrentTime;
        //}

    }
}
