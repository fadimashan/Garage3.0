using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    public class GeneralInfoModel
    {

        private DateTime? dateCreated;
        private DateTime? test;

        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string RegistrationNum { get; set; }

        public DateTime EnteringTime
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan TotalTimeParked { get; set; }

        //public int Time { get {  return test ??   }; set; }
    }
}
