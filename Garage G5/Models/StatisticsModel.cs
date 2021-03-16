using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class StatisticsModel
    {
        [Display(Name = "Amount of parked per vehicle type")]
        public Dictionary<string,int> VehicleTypeCounter { get; set; }
        [Display(Name = "Total amount of wheels in the garage")]
        public int TotalAmountOfWheels { get; set; }
        [Display(Name = "Total revenue")]
        public int TotalRevenue { get; set; }
        [Display(Name = "First vehicle arrival")]
        public DateTime? LongestParkedVehicleDate { get; set; }
        public string LongestParkedVehicleRegNo { get; set; }
        [Display(Name = "Latest vehicle arrival")]
        public DateTime? LatestParkedVehicleDate { get; set; }
        public string LatestParkedVehicleRegNo { get; set; }
    }
}
