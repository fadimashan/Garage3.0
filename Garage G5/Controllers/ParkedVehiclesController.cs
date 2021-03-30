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
using AutoMapper;
using Microsoft.AspNetCore.Http;

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

        public async Task<IActionResult> Receipt(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            var member = await _context.Member.FindAsync(parkedVehicle.MemberId);
            var vehicle = await _context.TypeOfVehicle.FindAsync(parkedVehicle.TypeOfVehicleId);


            if (parkedVehicle == null)
            {
                return NotFound();
            }
            else
            {
                var nRM = new ReceiptModel
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = vehicle.TypeName,
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
            }
            else if (model.MembershipType == MembershipType.Pro)
            {
                discount = 5;
            }
            else
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
        public Dictionary<string, int> VehicleTypeCounter()
        {
            List<string> names = new List<string>();
            var list = _context.ParkedVehicle.ToList();
            var listOfTypes = new Dictionary<string, int>();
            var list2 = _context.TypeOfVehicle.ToList();
            foreach (var item in list2)
            {
                names.Add(item.TypeName);

            }

            foreach (string type in names)
            {
                int count = list.Count(ve => ve.TypeOfVehicle.TypeName.ToString() == type);
                listOfTypes.Add(type, count);
            }
            return (listOfTypes);
        }


        // GET: ParkedVehicles/Statistics
        public async Task<IActionResult> Statistics()
        {
            var vehicles = await _context.ParkedVehicle.ToListAsync();
            var types = await _context.TypeOfVehicle.ToListAsync();

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
                    //Disabled = CheckFreePlaces(),
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



        private void CheckAvailability()
        {
            int garageCapacity = 100;
            int freePlaces = 0;
            var placeCounter = _context.ParkedVehicle.Where(v => v.IsInGarage == true).Where(v=> v.TypeOfVehicle.TypeName != "Motorcycle").Select(v => v.TypeOfVehicle.Size).Sum();
            var motorcyclePlaces = _context.ParkedVehicle.Where(v => v.IsInGarage == true).Where(v=> v.TypeOfVehicle.TypeName == "Motorcycle").Select(v => v.TypeOfVehicle.Size).Sum();

            int motorCapasty;

            if (motorcyclePlaces != 0 && motorcyclePlaces > 3 && motorcyclePlaces % 3 != 0)
            {
                placeCounter = placeCounter + (int)(motorcyclePlaces / 3);
            }
            if (motorcyclePlaces != 0 && motorcyclePlaces % 3 == 0)
            {
                placeCounter = placeCounter + (motorcyclePlaces / 3);
            }

            if (motorcyclePlaces != 0 && motorcyclePlaces % 3 > 0)
            {
                placeCounter++;
            }

            if (garageCapacity == (placeCounter - 1) && motorcyclePlaces % 3 > 0)
            {
                placeCounter++;
            }

            var restOftheMotor = motorcyclePlaces % 3;
            freePlaces = garageCapacity - placeCounter;
            motorCapasty = (restOftheMotor != 0) ? freePlaces * 3 + (3 - restOftheMotor) : freePlaces * 3;

            HttpContext.Session.SetInt32("FreePlaces", freePlaces);
            HttpContext.Session.SetInt32("MotorFreePlaces", motorCapasty);
        }

        public async Task<IActionResult> GeneralInfoGarage(VehicleFilterViewModel viewModel, string inputString)
        {
            CheckAvailability();
            var vehicles = string.IsNullOrWhiteSpace(inputString) ?
            _context.ParkedVehicle.Where(v => v.IsInGarage == true ):
            _context.ParkedVehicle.Where(v => v.IsInGarage == true && (v.RegistrationNum.StartsWith(inputString) || v.Brand.StartsWith(inputString)));

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

            if (viewModel.TypeOfVehicle != null)
            {
                vehicles = viewModel.TypeOfVehicle.TypeName == null ?
                vehicles :
                vehicles.Where(m => m.TypeOfVehicle.TypeName == viewModel.TypeOfVehicle.TypeName);
            }

            var general = mapper.ProjectTo<GeneralInfoViewModel>(vehicles);


            var list = new VehicleFilterViewModel
            {
                GetVehiclesType = await GetVehicleTypeAsync(),
                GeneralVehicles = await general.ToListAsync()
            };


            return View("GeneralInfoGarage", list);

        }


        //This is a sorting function
        public async Task<IActionResult> Index(string sortOrder)
        {
            CheckAvailability();
            ViewBag.RegSortParm = (sortOrder == "RegistrationNum") ? $"{sortOrder}_desc" : "RegistrationNum";
            ViewBag.DateSortParm = (sortOrder == "EntryDate") ? $"{sortOrder}_desc" : "EntryDate";
            ViewBag.VehicleTypeSortParm = (sortOrder == "VehicleType") ? $"{sortOrder}_desc" : "VehicleType";
            ViewBag.MemberSortParm = (sortOrder == "Member") ? $"{sortOrder}_desc" : "Member";
            ViewBag.MemberTypeSortParm = (sortOrder == "MemberType") ? $"{sortOrder}_desc" : "MemberType";
            ViewBag.TotalTimeSortParm = (sortOrder == "TotalTime") ? $"{sortOrder}_desc" : "TotalTime";

            var vehicles = from v in _context.ParkedVehicle
                           where v.IsInGarage == true
                           select v;
            switch (sortOrder)
            {
                case "RegistrationNum":
                    vehicles = vehicles.OrderBy(v => v.RegistrationNum);
                    break;
                case "RegistrationNum_desc":
                    vehicles = vehicles.OrderByDescending(v => v.RegistrationNum);
                    break;
                case "VehicleType":
                    vehicles = vehicles.OrderBy(v => v.TypeOfVehicle.TypeName);
                    break;
                case "VehicleType_desc":
                    vehicles = vehicles.OrderByDescending(v => v.TypeOfVehicle.TypeName);
                    break;
                case "MemberType":
                    vehicles = vehicles.OrderBy(v => v.Member.MembershipType);
                    break;
                case "MemberType_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Member.MembershipType);
                    break;
                case "Member":
                    vehicles = vehicles.OrderBy(v => v.Member.FirstName);
                    break;
                case "Member_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Member.FirstName);
                    break;
                case "TotalTime":
                    vehicles = vehicles.OrderBy(v => v.EnteringTime);
                    break;
                case "TotalTime_desc":
                    vehicles = vehicles.OrderByDescending(v => v.EnteringTime);
                    break;

            }

            var general = mapper.ProjectTo<GeneralInfoViewModel>(vehicles);


            var list = new VehicleFilterViewModel
            {
                GetVehiclesType = await GetVehicleTypeAsync(),
                GeneralVehicles = general.ToList()
            };

            return View("GeneralInfoGarage", list);
        }



        public IEnumerable<GeneralInfoViewModel> Reg(string reg = null)
        {
            var model = _context.ParkedVehicle.Select(x => new GeneralInfoViewModel
            {
                Id = x.Id,
                RegistrationNum = x.RegistrationNum,
                TypeOfVehicle = x.TypeOfVehicle,
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
                TypeOfVehicle = x.TypeOfVehicle,
                EnteringTime = x.EnteringTime,
            }).ToList();

            return (model);
        }

        private async Task<IEnumerable<SelectListItem>> GetVehicleTypeAsync()
        {
            var TypeName = await _context.TypeOfVehicle.ToListAsync();
            var GetTypeOfVehicle = new List<SelectListItem>();
            foreach (var type in TypeName)
            {
                var newType = (new SelectListItem
                {
                    Text = type.TypeName,
                    Value = type.TypeName,
                });
                GetTypeOfVehicle.Add(newType);
            }
            return ( GetTypeOfVehicle);
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
