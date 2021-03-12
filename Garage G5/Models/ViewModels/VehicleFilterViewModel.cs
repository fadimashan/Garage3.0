using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models.ViewModels
{
    [Keyless]
    public class VehicleFilterViewModel
    {
        //private DateTime? dateCreated;
        //public IEnumerable<ParkedVehicle> Vehicles { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public List<GeneralInfoModel> GenralVehicles { get; set; }

        public VehicleType VehicleType { get; set; }

        //    public VehicleType? Type { get; set; }
        public string RegistrationNum { get; set; }

        //    public int Id { get; set; }
        //public VehicleType VehicleType { get; set; }

        //    public DateTime EnteringTime
        //    {
        //        get { return dateCreated ?? DateTime.Now; }
        //        set { dateCreated = value; }
        //    }
        //    public TimeSpan TotalTimeParked { get; set; }
        //}
    }
}
