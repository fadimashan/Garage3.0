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

        //public async Task<IActionResult> Index(GeneralInfoViewModel viewModel)
        //{
        //    var vehicles = string.IsNullOrWhiteSpace(viewModel.RegistrationNum) ?
        //       _context.ParkedVehicle :
        //       _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(viewModel.RegistrationNum));

        //    vehicles = viewModel.VehicleType == null ?
        //     vehicles :
        //     vehicles.Where(m => m.VehicleType == viewModel.VehicleType);

        //    var model = new GeneralInfoViewModel
        //    {
        //        ParkedVehicles = vehicles,
        //        Types = await GetGenresAsync()
        //    };

        //    return View(model);

        //}

        private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await _context.ParkedVehicle
                          .Select(m => m.VehicleType)
                          .Distinct()
                          .Select(g => new SelectListItem
                          {
                              Text = g.ToString(),
                              Value = g.ToString()
                          })
                          .ToListAsync();
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
                var nRM = new ReceiptModel
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = parkedVehicle.VehicleType,
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
                return RedirectToAction(nameof(SearchAndFilterView));
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
                return RedirectToAction(nameof(SearchAndFilterView));
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
            return RedirectToAction(nameof(SearchAndFilterView));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
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


        public async Task<IActionResult> SearchAndFilterView(VehicleFilterViewModel viewModel, string RegistrationNum)
        {

            var vehicles = string.IsNullOrWhiteSpace(RegistrationNum) ?
            _context.ParkedVehicle :
            _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(RegistrationNum));

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
                Types = await GetCategoryAsync(),
                GenralVehicles = geniral.ToList()

            };

            return View("SearchAndFilterView", list);
        }

        public IEnumerable<GeneralInfoViewModel> Reg( string reg = null)
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
