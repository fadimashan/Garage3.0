using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class TypeOfVehicle
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Type of vehicle")]
        public string TypeName { get; set; }
        //public int? ExtraRate { get; set; }
        //public int? ExtraHourlyRate { get; set; }
        [Range(1, 5)]
        public int Size { get; set; }

        //public List<TypeOfVehicle> TypeOfVehicles { get; set; }
        //public ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}
