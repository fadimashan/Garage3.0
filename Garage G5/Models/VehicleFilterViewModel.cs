using Microsoft.AspNetCore.Mvc;
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
    public class VehicleFilterViewModel
    {

        //public int Id { get; set; }

        public List<GeneralInfoViewModel> GeneralVehicles { get; set; }
        [Display(Name = "Registration")]
        public IEnumerable<GeneralInfoViewModel> GeneralRegistration { get; set; }

        [Display(Name = "Vehicle Type")]
        public TypeOfVehicle TypeOfVehicle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RegistrationNum { get; set; }

        public IEnumerable<SelectListItem> GetVehiclesType { get; set; }

    }
}
