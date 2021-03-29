using System;
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
using Microsoft.AspNetCore.Http;
using AutoMapper;
using PagedList;
using Garage_G5.Extension;

namespace Garage_G5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_G5Context _context;
        private readonly IMapper mapper;

        public ParkedVehiclesController(Garage_G5Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;

        }

        //public IEnumerable<SelectListItem> GetVehiclesType()
        //{
        //    int value;
        //    var getVehiclesType = new List<SelectListItem>();
        //    foreach (var type in Enum.GetNames(typeof(VehicleType)))
        //    {
        //        value = (int)Enum.Parse(typeof(VehicleType), type, true);
        //        var newType = (new SelectListItem
        //        {
        //            Text = type.ToString(),
        //            Value = type.ToString(),
        //            Disabled = CheckFreePlaces(value),
        //        });
        //        getVehiclesType.Add(newType);
        //    }
        //    return (getVehiclesType);
        //}

        //private bool CheckFreePlaces(int val)
        //{
        //    var freePlaces = HttpContext.Session.GetInt32("FreePlaces");
        //    switch (val)
        //    {
        //        case (int)VehicleType.Sedan:
        //        case (int)VehicleType.Combi:
        //        case (int)VehicleType.Coupe:
        //        case (int)VehicleType.Roadster:
        //        case (int)VehicleType.MiniVan:
        //        case (int)VehicleType.Van:
        //            if (freePlaces >= 1)
        //            {
        //                return false;
        //            }
        //            else return true;
        //        case (int)VehicleType.Truck:
        //        case (int)VehicleType.BigTruck:
        //            if (freePlaces >= 2)
        //            {
        //                return false;
        //            }
        //            else return true;
        //        case (int)VehicleType.Boat:
        //        case (int)VehicleType.Airplane:
        //            if (freePlaces >= 3)
        //            {
        //                return false;
        //            }
        //            else return true;
        //        default:
        //            return false;
        //    }
        //}


        //public Dictionary<string, int> VehicleTypeCounter()
        //{

        //    var list = _context.ParkedVehicle.ToList();
        //    var listOfTypes = new Dictionary<string, int>();

        //    foreach (string type in Enum.GetNames(typeof(VehicleType)))
        //    {
        //        int count = list.Count(ve => ve.TypeOfVehicle.TypeName.ToString() == type);
        //        listOfTypes.Add(type, count);
        //    }
        //    return (listOfTypes);
        //}


        public async Task<IActionResult> Receipt(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            var member = await _context.Member.FindAsync(parkedVehicle.MemberId);


            if (parkedVehicle == null)
            {
                return NotFound();
            }
            else
            {
                var nRM = new ReceiptModel
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = parkedVehicle.TypeOfVehicle.TypeName,
                    Id = parkedVehicle.Id,
                    EnteringTime = parkedVehicle.EnteringTime,
                    TotalTimeParked = DateTime.Now - parkedVehicle.EnteringTime,
                    Price = getPrice(parkedVehicle.EnteringTime),
                    Fullname = member.FullName,
                    MembershipType = member.MembershipType,
                    Discount = (getDiscount(member)),
                    TotalPrice = getPrice(parkedVehicle.EnteringTime) - (int)(getDiscount(member) * getPrice(parkedVehicle.EnteringTime))
                };
                return View(nRM);
            }
        }

        private int getPrice(DateTime entring)
        {
            //Vehicles that take up a place basic fee +hourly rate* time
            //Vehicles that take up two places basic fee *1.3 + hourly price * 1.4 * time
            //Vehicles that charge three or more basic fee *1.6 + hourly rate * 1.5 * time
            var price = (DateTime.Now - entring).TotalMinutes * 10 / 60;
            return (int)price;
        }

        private int getDiscount(Member model)
        {
            int discount;
            if (model.MembershipType == MembershipType.Regular) 
            {
                discount = 0;
            } else if(model.MembershipType == MembershipType.Pro) 
            {
                discount = 5;
            } else
            {
                discount = 10;
            }

            return discount;
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
                //nSM.VehicleTypeCounter = VehicleTypeCounter();

            }

            return View(nSM);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            var model = new ParkedVehicle
            {
                //GetVehiclesType = GetVehiclesType()
                GetVehiclesType = GetTypeOfVehicle(),
                IsInGarage = false
            };

            return View(model);
        }

        //New Method
        private IEnumerable<SelectListItem> GetTypeOfVehicle()
        {
            var TypeName = _context.TypeOfVehicle;
            var GetTypeOfVehicle = new List<SelectListItem>();
            foreach (var type in TypeName)
            {
                var newType = (new SelectListItem
                {
                    Text = type.TypeName,
                    Value = type.Id.ToString(),
                    //Disabled = CheckFreePlaces(type.Id),
                });
                GetTypeOfVehicle.Add(newType);
            }
            return (GetTypeOfVehicle);
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
                return RedirectToAction(nameof(GeneralInfoGarage), new { @notify = "parked" });
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

            parkedVehicle.GetVehiclesType = GetTypeOfVehicle();
            return View(parkedVehicle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime,MemberId,IsInGarage")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    parkedVehicle.MemberId = 1;
                    parkedVehicle.IsInGarage = true;
                    _context.Update(parkedVehicle);
                    // _context.Entry(parkedVehicle).Property(x => x.EnteringTime).IsModified = false;
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
                return RedirectToAction(nameof(GeneralInfoGarage), new { @notify = "edit" });
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
            return RedirectToAction(nameof(GeneralInfoGarage), new { @notify = "checkout" });
        }

        public async Task<IActionResult> CheckOutConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            parkedVehicle.IsInGarage = false;
            parkedVehicle.EnteringTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GeneralInfoGarage), new { @notify = "checkout" });
        }


        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
        private void CheckAvailability2() {

            var listAllVehicles = _context.ParkedVehicle.Where(v => v.IsInGarage == true).ToList();
            var listOfsize = _context.TypeOfVehicle.ToList();
            Dictionary<string, int> totalSize = new Dictionary<string, int>();

            foreach (var s in listOfsize)
            {
                string name = "" ;
                var counter = 0;
                foreach (var v in listAllVehicles)
                {
                    if (s.TypeName == v.TypeOfVehicle.TypeName)
                    {
                        name = s.TypeName;
                        counter++;
                    }
                }
                totalSize.Add(name, counter);
            }

            totalSize.Add("theEnd",0);
        }



        //private void CheckAvailability()
        //{
        //    int garageCapacity = 158;
        //    int motorcyclePlaces = 0;
        //    var listAllVehicles = _context.ParkedVehicle.Where(v => v.IsInGarage == true).ToList();
        //    int placeCounter = 0;
        //    int motorCapasty;
        //    int freePlaces;

        //    foreach (var vehicle in listAllVehicles)
        //    {
        //        switch (vehicle.TypeOfVehicle.TypeName)
        //        {
        //            case TypeOfVehicle.TypeName.Motorcycle:
        //                motorcyclePlaces = motorcyclePlaces + 1;
        //                break;

        //            case VehicleType.Sedan:
        //            case VehicleType.Combi:
        //            case VehicleType.Coupe:
        //            case VehicleType.Roadster:
        //            case VehicleType.MiniVan:
        //            case VehicleType.Van:
        //                placeCounter++;
        //                break;

        //            case VehicleType.Truck:
        //            case VehicleType.BigTruck:
        //                placeCounter = placeCounter + 2;
        //                break;
        //            case VehicleType.Boat:
        //            case VehicleType.Airplane:
        //                placeCounter = placeCounter + 3;
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    if (motorcyclePlaces != 0 && motorcyclePlaces > 3 && motorcyclePlaces % 3 != 0)
        //    {
        //        placeCounter = placeCounter + (int)(motorcyclePlaces / 3);
        //    }
        //    if (motorcyclePlaces != 0 && motorcyclePlaces % 3 == 0)
        //    {
        //        placeCounter = placeCounter + (motorcyclePlaces / 3);
        //    }

        //    if (motorcyclePlaces != 0 && motorcyclePlaces % 3 > 0)
        //    {
        //        placeCounter++;
        //    }

        //    if (garageCapacity == (placeCounter - 1) && motorcyclePlaces % 3 > 0)
        //    {
        //        placeCounter++;
        //    }

        //    var restOftheMotor = motorcyclePlaces % 3;
        //    freePlaces = garageCapacity - placeCounter;
        //    motorCapasty = (restOftheMotor != 0) ? freePlaces * 3 + (3 - restOftheMotor) : freePlaces * 3;


        //    HttpContext.Session.SetInt32("FreePlaces", freePlaces);
        //    HttpContext.Session.SetInt32("MotorFreePlaces", motorCapasty);

        //}

        public async Task<IActionResult> GeneralInfoGarage(VehicleFilterViewModel viewModel, string RegistrationNum)
        {
            //CheckAvailability();
            //CheckAvailability2();

            var vehicles = string.IsNullOrWhiteSpace(RegistrationNum) ?
            _context.ParkedVehicle :
            _context.ParkedVehicle.Where(v => v.RegistrationNum.StartsWith(RegistrationNum) || v.Brand.StartsWith(RegistrationNum));


            vehicles = viewModel.VehicleType == null ?
                vehicles :
                vehicles.Where(m => m.TypeOfVehicle.TypeName == viewModel.VehicleType);

            vehicles.Where(v => v.IsInGarage == true).ToList();

            var types = _context.TypeOfVehicle.ToList();
            foreach (var v in vehicles)
            {

                foreach (var t in types)
                {
                    if (v.TypeOfVehicleId == t.Id)
                    {
                        v.TypeOfVehicle = t;
                    }
                }

                await _context.SaveChangesAsync();
            }

            //var geniral = mapper.ProjectTo<GeneralInfoViewModel>(vehicles).Where(v => v.IsInGarage == true);

            var geniral = _context.ParkedVehicle.Select(x => new GeneralInfoViewModel
            {
                IsInGarage = x.IsInGarage,
                VehicleType = x.TypeOfVehicle.TypeName,
                EnteringTime = x.EnteringTime,
                RegistrationNum = x.RegistrationNum

            });

            var list = new VehicleFilterViewModel
            {
                Types = await GetVehicleTypeAsync(),
                GenralVehicles = geniral.ToList()
            };

            return View("GeneralInfoGarage", list);
        }

        //This is a sorting function
        public async Task<IActionResult> Index(string sortOrder)
        {
            //CheckAvailability();
            //CheckAvailability2();
            ViewBag.RegSortParm = (sortOrder == "RegistrationNum") ? $"{sortOrder}_desc" : "RegistrationNum";
            ViewBag.DateSortParm = (sortOrder == "EntryDate") ? $"{sortOrder}_desc" : "EntryDate";
            ViewBag.VehicleTypeSortParm = (sortOrder == "VehicleType") ? $"{sortOrder}_desc" : "VehicleType";
            ViewBag.TotalTimeSortParm = (sortOrder == "TotalTime") ? $"{sortOrder}_desc" : "TotalTime";

            var vehicles = from v in _context.ParkedVehicle
                           //where v.IsInGarage == true
                           //join t in _context.TypeOfVehicle 
                           //on v.TypeOfVehicleId equals t.Id
                           select v;
            switch (sortOrder)
            {
                case "RegistrationNum":
                    vehicles = vehicles.OrderBy(v => v.RegistrationNum);
                    break;
                case "RegistrationNum_desc":
                    vehicles = vehicles.OrderByDescending(v => v.RegistrationNum);
                    break;

                case "EntryDate":
                    vehicles = vehicles.OrderBy(v => v.EnteringTime);
                    break;
                case "EntryDate_desc":
                    vehicles = vehicles.OrderByDescending(v => v.EnteringTime);
                    break;
                case "VehicleType":
                    vehicles = vehicles.OrderBy(v => v.TypeOfVehicle.TypeName);
                    break;
                case "VehicleType_desc":
                    vehicles = vehicles.OrderByDescending(v => v.TypeOfVehicle.TypeName);
                    break;
                case "TotalTime":
                    vehicles = vehicles.OrderBy(v => v.EnteringTime);
                    break;
                case "TotalTime_desc":
                    vehicles = vehicles.OrderByDescending(v => v.EnteringTime);
                    break;

            }

            //var types = _context.TypeOfVehicle.ToList();
            //foreach (var v in vehicles)
            //{
            //    foreach (var t in types)
            //    {
            //        if (v.TypeOfVehicleId == t.Id )
            //        {
            //            v.TypeOfVehicle = t;
            //        }
            //    }
            //   await _context.SaveChangesAsync();
            //}
            
            //var jdfs = mapper.ProjectTo<>
            var geniral = _context.ParkedVehicle.Select(x => new GeneralInfoViewModel
            {
                IsInGarage = x.IsInGarage,
                VehicleType = x.TypeOfVehicle.TypeName,
                EnteringTime = x.EnteringTime,
                RegistrationNum = x.RegistrationNum

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
                VehicleType = x.TypeOfVehicle.TypeName,
                EnteringTime = x.EnteringTime,
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
                VehicleType = x.TypeOfVehicle.TypeName,
                EnteringTime = x.EnteringTime,
            }).ToList();

            return (model);
        }



        private async Task<IEnumerable<SelectListItem>> GetVehicleTypeAsync()
        {
            var g = _context.ParkedVehicle;
            return await _context.ParkedVehicle
                .Select(p => p.TypeOfVehicle.TypeName)
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
