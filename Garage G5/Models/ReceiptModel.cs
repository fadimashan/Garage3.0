using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Garage_G5.Data;
using Garage_G5.Models;
namespace Garage_G5.ViewModels
{
    public class ReceiptModel
    {
        private DateTime? receiptCreated;
        public int Price { get; set; }

        public int Id { get; set; }
        [Display(Name = "Departure")]
        public DateTime CheckoutTime
        {
            get { return receiptCreated ?? DateTime.Now; }
            set { receiptCreated = value; }
        }
        [Display(Name = "Arrival")]
        public DateTime EnteringTime { get; set; }
        [Display(Name = "Time Parked")]
        public TimeSpan TotalTimeParked { get; set; }
        [Display(Name = "Registration")]
        public string RegistrationNum { get; set; }
        [Display(Name = "Type")]
        public VehicleType VehicleType { get; set; }

    }
}
