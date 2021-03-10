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
        public int Price { get; set; }
        public DateTime Receipt { get; set; }
        public string VehicleRegistrationNum { get; }
        public string VehicleType { get; }

        //1. Skapa vymodell med den information kvittot behöver!
        //2. Skapa en metod på kontrollern för att generera ett kvitto
        //3. Hämta den inforamtionen du behöver från DB
        //4. Mappa den informationen till Vymodellen
        //5. Sätt värderna på de saker som inte kommer från databasen (tex kostnad)
        //6.  Skapa en vy med modellen som är din vymodell
        //7. Ornda så man kan komma till din backend och skicka med det du behöver tex id


    }
}
