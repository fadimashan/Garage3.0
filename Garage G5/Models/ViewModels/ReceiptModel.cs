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
        //checkout time should be now (the time that you create the receipt)
        public DateTime CheckoutTime
        {
            get { return receiptCreated ?? DateTime.Now; }
            set { receiptCreated = value; }
        }

        //this Entering time should be the same from the database 
        public DateTime EnteringTime { get; set; }
        public TimeSpan TotalTimeParked { get; set; }
        public string RegistrationNum { get; set; }
        public VehicleType VehicleType { get; set; }



        // this should be in the controller ( see the example )
        // and this prop should be int or duoble
        //public decimal CalcTotal()
        //{

        //}
     
        //public DateTime CalcTotal { get; set; }



    }
    
   
}
//1. Skapa vymodell med den information kvittot behöver
//2. Skapa en metod på kontrollern för att generera ett kvitto
//3. Hämta den inforamtionen du behöver från DB
//4. Mappa den informationen till Vymodellen
//5. Sätt värderna på de saker som inte kommer från databasen (tex kostnad)
//6. Skapa en vy med modellen som är din vymodell
//7. Ordna så man kan komma till din backend och skicka med det du behöver tex id