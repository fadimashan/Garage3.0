using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Models
{
    public class StatisticsModel
    {
        public int VehicleTypeCounter { get; set; }
        public int TotalAmountOfWheels { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime LongestParkedVehicleDate { get; set; }
        public int LongestParkedVehicleId { get; set; }
        public DateTime LatestParkedVehicleDate { get; set; }
        public int LatestParkedVehicleId { get; set; }
    }
}
