//using Garage_G5.Data;
//using Garage_G5.Models.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Garage_G5.Services
//{
//    public class TotalParkedTimeService : ITotalParkedTimeSeravice
//    {
//        private readonly Garage_G5Context db;

//        public TotalParkedTimeService(Garage_G5Context db)
//        {
//            this.db = db;
//        }

//        public async Task<IEnumerable<string> GetTotalPakedTime()
//        {


//            var model = db.ParkedVehicle.Select(x => new GeneralInfoViewModel
//            {
               
//                RegistrationNum = x.RegistrationNum,
//                VehicleType = x.VehicleType,
//                EnteringTime = x.EnteringTime,
//                TotalParkedTime = DateTime.Now - x.EnteringTime,
//            });
//        TimeSpan tm = new TimeSpan();
//            foreach (var item in model)
//            {
//                tm = item.TotalParkedTime;

//            }
//    string v = tm.ToString();
           
//            return v;

           
//            }
//        }
//    }
//}

      
//}

//public Task<IEnumerable<TimeSpan>> GetTotalPakedTime(GeneralInfoViewModel viewModel)
//{
//    var time = db.ParkedVehicle.Where(m => m.EnteringTime == viewModel.EnteringTime);
//    var TotalParkedTime = (DateTime.Now - (time).;


//    {

//    }



//            {
                
//            )
//        }

//    }
 

