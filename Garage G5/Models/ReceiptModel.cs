using System;
using System.Collections.Generic;
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
        public DateTime CheckoutTime
        {
            get { return receiptCreated ?? DateTime.Now; }
            set { receiptCreated = value; }
        }

        public DateTime EnteringTime { get; set; }
        public TimeSpan TotalTimeParked { get; set; }
        public string RegistrationNum { get; set; }
        public VehicleType VehicleType { get; set; }

    }
}
