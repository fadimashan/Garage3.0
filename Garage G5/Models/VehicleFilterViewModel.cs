using Microsoft.AspNetCore.Mvc;
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
    
        public int Id { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public List<GeneralInfoViewModel> GenralVehicles { get; set; }
        public IEnumerable<GeneralInfoViewModel> GenralRegistration { get; set; }

        public VehicleType? VehicleType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RegistrationNum { get; set; }

    }
}
