using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Data;
using Garage_G5.Models;
using Garage_G5.ViewModels;
using Garage_G5.Models.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Garage_G5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_G5Context _context;
        public ParkedVehiclesController(Garage_G5Context context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetVehiclesType()
        {
            int value;
            var getVehiclesType = new List<SelectListItem>();
            foreach (var type in Enum.GetNames(typeof(VehicleType)))
            {
                value = (int)Enum.Parse(typeof(VehicleType), type, true);
                var newType = (new SelectListItem
                {
                    Text = type.ToString(),
                    Value = type.ToString(),
                    Disabled = CheckFreePlaces(value),
                });
                getVehiclesType.Add(newType);
            }
            return (getVehiclesType);
        }

        private bool CheckFreePlaces(int val)
        {
            var freePlaces = HttpContext.Session.GetInt32("FreePlaces");
            switch (val)
            {
                case (int)VehicleType.Sedan:
                case (int)VehicleType.Coupe:
                case (int)VehicleType.Roadster:
                case (int)VehicleType.MiniVan:
                case (int)VehicleType.Van:
                    if (freePlaces >= 1)
                    {
                        return false;
                    }
                    else return true;
                case (int)VehicleType.Truck:
                case (int)VehicleType.BigTruck:
                    if (freePlaces >= 2)
                    {
                        return false;
                    }
                    else return true;
                case (int)VehicleType.Boat:
                case (int)VehicleType.Airplane:
                    if (freePlaces >= 3)
                    {
                        return false;
                    }
                    else return true;
                default:
                    return false;
            }
        }


        public Dictionary<string, int> VehicleTypeCounter()
        {

            var list = _context.ParkedVehicle.ToList();
            var listOfTypes = new Dictionary<string, int>();

            foreach (string type in Enum.GetNames(typeof(VehicleType)))
            {
                int count = list.Count(ve => ve.VehicleType.ToString() == type);
                listOfTypes.Add(type, count);
            }
            return (listOfTypes);
        }


        public async Task<IActionResult> Receipt(int id)
        {


            if (id == 0)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            else
            {
                var nRM = new ReceiptModel
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = (VehicleType)parkedVehicle.VehicleType,
                    Id = parkedVehicle.Id,
                    EnteringTime = parkedVehicle.EnteringTime,
                    TotalTimeParked = DateTime.Now - parkedVehicle.EnteringTime,
                    Price = (int)(DateTime.Now - parkedVehicle.EnteringTime).TotalMinutes * 10 / 60,

                };
                return View(nRM);
            }
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }


        // GET: ParkedVehicles/Statistics
        public async Task<IActionResult> Statistics()
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();
            var nSM = new StatisticsModel();
            DateTime longestParked = DateTime.MaxValue;
            string longestParkedRegNo = "";
            foreach (var vehicle in vehicles)
            {
                nSM.TotalAmountOfWheels += vehicle.WheelsNum;
                var price = (int)(DateTime.Now - vehicle.EnteringTime).TotalMinutes * 10 / 60;
                nSM.TotalRevenue += price;
                if (longestParked > vehicle.EnteringTime)
                {
                    longestParked = vehicle.EnteringTime;
                    longestParkedRegNo = vehicle.RegistrationNum;

                }
                nSM.LongestParkedVehicleDate = longestParked;
                nSM.LongestParkedVehicleRegNo = longestParkedRegNo;
                nSM.VehicleTypeCounter = VehicleTypeCounter();

            }

            return View(nSM);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            var model = new ParkedVehicle
            {
                GetVehiclesType = GetVehiclesType()

            };
            return View(model);
        }
        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime")] ParkedVehicle parkedVehicle)
        {

            if (ModelState.IsValid)
            {
                parkedVehicle.EnteringTime = DateTime.Now;
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GeneralInfoGarage));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            parkedVehicle.GetVehiclesType = GetVehiclesType();
            return View(parkedVehicle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {

                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GeneralInfoGarage));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GeneralInfoGarage));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GeneralInfoGarage(VehicleFilterViewModel viewModel, string RegistrationNum)
        {
            int garageCapacity = 10;
            int freePlaces = 0;
            int motorcycleFreePlaces = 0;
            var listAllVehicles = _context.ParkedVehicle.ToList();
            int placeCounter = 0;
            foreach (var vehicle in listAllVehicles)
            {
                switch (vehicle.VehicleType)
                {
                    case VehicleType.Motorcycle:
                        motorcycleFreePlaces = motorcycleFreePlaces + 1;
                        break;

                    case VehicleType.Sedan:
                    case VehicleType.Combi:
                    case VehicleType.Coupe:
                    case VehicleType.Roadster:
                    case VehicleType.MiniVan:
                    case VehicleType.Van:
                        placeCounter++;
                        break;

                    case VehicleType.Truck:
                    case VehicleType.BigTruck:
                        placeCounter = placeCounter + 2;
                        break;
                    case VehicleType.Boat:
                    case VehicleType.Airplane:
                        placeCounter = placeCounter + 3;
                        break;
                    default:
                        break;
                }
            }

            if (motorcycleFreePlaces != 0 && motorcycleFreePlaces > 3 && motorcycleFreePlaces % 3 != 0)
            {
                placeCounter = placeCounter + (int)(motorcycleFreePlaces / 3);
            }
            if (motorcycleFreePlaces != 0 && motorcycleFreePlaces % 3 == 0)
            {
                placeCounter = placeCounter + (motorcycleFreePlaces / 3);
            }

            if (motorcycleFreePlaces != 0 && motorcycleFreePlaces % 3 > 0)
            {
                placeCounter++;
            }
            freePlaces = garageCapacity - placeCounter;

            HttpContext.Session.SetInt32("FreePlaces", freePlaces);
            var vehicles = string.IsNullOrWhiteSpace(RegistrationNum) ?
            _context.ParkedVehicle :
            _context.ParkedVehicle.Where(v => v.RegistrationNum.StartsWith(RegistrationNum) || v.Brand.StartsWith(RegistrationNum));

            vehicles = viewModel.VehicleType == null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleType);

            var geniral = vehicles.Select(x => new GeneralInfoViewModel
            {
                Id = x.Id,
                RegistrationNum = x.RegistrationNum,
                VehicleType = x.VehicleType,
                EnteringTime = x.EnteringTime,
                TotalTimeParked = DateTime.Now - x.EnteringTime
            });


            var list = new VehicleFilterViewModel
            {
                Types = await GetVehicleTypeAsync(),
                GenralVehicles = geniral.ToList()

            };

            return View("GeneralInfoGarage", list);
        }

        public IEnumerable<GeneralInfoViewModel> Reg(string reg = null)
        {
            var model = _context.ParkedVehicle.Select(x => new GeneralInfoViewModel
            {
                Id = x.Id,
                RegistrationNum = x.RegistrationNum,
                VehicleType = x.VehicleType,
                EnteringTime = x.EnteringTime,
                TotalTimeParked = DateTime.Now - x.EnteringTime,
            }).ToList();

            return from v in model
                   where string.IsNullOrEmpty(reg) || v.RegistrationNum.StartsWith(reg)
                   orderby v.RegistrationNum
                   select v;


        }


        public IEnumerable<GeneralInfoViewModel> GeneralInfoModel()
        {
            var model = _context.ParkedVehicle.Select(x => new GeneralInfoViewModel
            {
                Id = x.Id,
                RegistrationNum = x.RegistrationNum,
                VehicleType = x.VehicleType,
                EnteringTime = x.EnteringTime,
                TotalTimeParked = DateTime.Now - x.EnteringTime,
            }).ToList();

            return (model);
        }



        private async Task<IEnumerable<SelectListItem>> GetVehicleTypeAsync()
        {
            return await _context.ParkedVehicle
                .Select(p => p.VehicleType)
                .Distinct()
                .Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString(),
                })
                .ToListAsync();
        }

        public bool IsRegisterNumberExists(string RegistrationNum, int Id)
        {
            if (Id == 0)
            {
                return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum);
            }
            else
            {
                return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum && x.Id != Id);

            }
        }

        /* Anther way to check if the RegistrationNum is unique */

        //[AcceptVerbs("GET", "POST")]
        //public IActionResult IsRegExists(string RegistrationNum, int Id)
        //{
        //    return Json(IsUnique(RegistrationNum, Id));
        //}

        //private bool IsUnique(string RegistrationNum, int Id)
        //{
        //    if (Id == 0) // its a new object
        //    {
        //        return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum);
        //    }
        //    else 
        //    {
        //        return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum && x.Id != Id);
        //    }
        //}
    }

}
