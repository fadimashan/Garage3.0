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

namespace Garage_G5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_G5Context _context;
        public ParkedVehiclesController(Garage_G5Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }


        public async Task<IActionResult> ReceiptModel(int id)
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
                var nRM = new ReceiptModel()
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = parkedVehicle.VehicleType,
                    Id = parkedVehicle.Id,
                    EnteringTime = parkedVehicle.EnteringTime,
                    TotalTimeParked = DateTime.Now - parkedVehicle.EnteringTime,
                    Price = (int)(DateTime.Now - parkedVehicle.EnteringTime).TotalMinutes * 10 / 60,
                    //Price = (int)
                    
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

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
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
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
  
        public bool IsRegisterNumberExists(string RegistrationNum)
        {
            return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum);
        }

        public ActionResult Filter(string registrationNum)
        {
            var listOfP = _context.ParkedVehicle.ToList();
            if (registrationNum != null)
            {
                var tempList = listOfP.Where(p => p.RegistrationNum.ToLower().Contains(registrationNum)).ToList();
                return View("Index", tempList);

            }
            return View("Index", _context.ParkedVehicle.ToList());

        }

        //public async Task<IActionResult> SearchAndFilter(VehicleFilterViewModel viewModel)
        //{
        //    var vehicles = string.IsNullOrWhiteSpace(viewModel.RegistrationNum) ?
        //        _context.ParkedVehicle :
        //        _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(viewModel.RegistrationNum));

        //    vehicles = viewModel.Type == null ?
        //        vehicles :
        //    vehicles.Where(m => m.VehicleType == viewModel.Type);

        //    var model = new VehicleFilterViewModel
        //    {
        //        Vehicles = vehicles,
        //        Types = await GetCategoryAsync()
        //    };

        //    return View(nameof(SearchAndFilterView), model);

        //}

        //public async Task<IActionResult> SearchAndFilterView(string Registration)
        //{

        //    var vehicles = string.IsNullOrWhiteSpace(Registration) ?
        //    _context.ParkedVehicle :
        //    _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(Registration));

        //    //vehicles =  vehicles.Where(m => m.VehicleType == viewModel.VehicleType).
        //    //Where(x => x.EnteringTime == viewModel.EnteringTime);
        //    var geniral = vehicles.Select(x => new GeneralInfoModel
        //    {
        //        RegistrationNum = x.RegistrationNum,
        //        VehicleType = x.VehicleType,
        //        EnteringTime = x.EnteringTime,
        //        TotalTimeParked = DateTime.Now - x.EnteringTime
        //    });

        //    //var model = GeneralInfoModel();

        //    var list = new VehicleFilterViewModel
        //    {
        //        Types = await GetCategoryAsync(),
        //        GenralVehicles = geniral.ToList()
        //    };

        //    return View("SearchAndFilterView", list);
        //}

        public async Task<IActionResult> SearchAndFilterView(VehicleFilterViewModel viewModel, string reistration)
        {

            var vehicles = string.IsNullOrWhiteSpace(reistration) ?
            _context.ParkedVehicle :
            _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(reistration));

            vehicles = viewModel.VehicleType == null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleType);
            
            var geniral = vehicles.Select(x => new GeneralInfoModel
            {
                RegistrationNum = x.RegistrationNum,
                VehicleType = x.VehicleType,
                EnteringTime = x.EnteringTime,
                TotalTimeParked = DateTime.Now - x.EnteringTime
            });


            var list = new VehicleFilterViewModel
            {
                Types = await GetCategoryAsync(),
                GenralVehicles = geniral.ToList()
            };

            return View("SearchAndFilterView", list);
        }



        public IEnumerable<GeneralInfoModel> GeneralInfoModel()
        {
            var model = _context.ParkedVehicle.Select(x => new GeneralInfoModel
            {
                Id = x.Id,
                RegistrationNum = x.RegistrationNum,
                VehicleType = x.VehicleType,
                EnteringTime = x.EnteringTime,
                TotalTimeParked = DateTime.Now - x.EnteringTime,
            }).ToList();

            return (model);
        }


        //public async Task<IActionResult> VehicleFilterViewModel(VehicleFilterViewModel viewModel)
        //{

        //    var model =  new GeneralInfoModel
        //    {

        //        Vehicles = SearchAndFilterView();
        //        vehicles,
        //        Types = await GetCategoryAsync()
        //    };

        //    return View(nameof(GeneralInfoModel), model);
        //}




        //public async Task<IActionResult> SearchAndFilterView()
        //{

        //    var vehicles = await _context.ParkedVehicle.ToArrayAsync();
        //    var model = new VehicleFilterViewModel()
        //    {
        //        Vehicles = vehicles,
        //        Types = await GetCategoryAsync()

        //    };

        //    return View(model);
        //}

        private async Task<IEnumerable<SelectListItem>> GetCategoryAsync()
        {
            return await _context.ParkedVehicle
                .Select(p => p.VehicleType)
                .Distinct()
                .Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString()
                })
                .ToListAsync();
        }

    }
}
