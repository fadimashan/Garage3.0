using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class StatisticsModel
    {
        public int VehicleTypeCounter { get; set; }
        [Display(Name = "Total amount of wheels in the garage")]
        public int TotalAmountOfWheels { get; set; }
        [Display(Name = "Revenue from currently parked vehicles")]
        public int TotalRevenue { get; set; }
        [Display(Name = "Date when the first vehicle arrived")]
        public DateTime LongestParkedVehicleDate { get; set; }
        public int LongestParkedVehicleId { get; set; }
        [Display(Name = "Date when the latest vehicle arrived")]
        public DateTime LatestParkedVehicleDate { get; set; }
        public int LatestParkedVehicleId { get; set; }
    }
}
