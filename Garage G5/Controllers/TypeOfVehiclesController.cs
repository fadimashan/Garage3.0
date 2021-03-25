using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Data;
using Garage_G5.Models;

namespace Garage_G5.Controllers
{
    public class TypeOfVehiclesController : Controller
    {
        private readonly Garage_G5Context _context;

        public TypeOfVehiclesController(Garage_G5Context context)
        {
            _context = context;
        }

        // GET: TypeOfVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfVehicle.ToListAsync());
        }

        // GET: TypeOfVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfVehicle = await _context.TypeOfVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfVehicle == null)
            {
                return NotFound();
            }

            return View(typeOfVehicle);
        }

        // GET: TypeOfVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,ExtraRate,ExtraHourlyRate,Size")] TypeOfVehicle typeOfVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfVehicle);
        }

        // GET: TypeOfVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfVehicle = await _context.TypeOfVehicle.FindAsync(id);
            if (typeOfVehicle == null)
            {
                return NotFound();
            }
            return View(typeOfVehicle);
        }

        // POST: TypeOfVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,ExtraRate,ExtraHourlyRate,Size")] TypeOfVehicle typeOfVehicle)
        {
            if (id != typeOfVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfVehicleExists(typeOfVehicle.Id))
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
            return View(typeOfVehicle);
        }

        // GET: TypeOfVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfVehicle = await _context.TypeOfVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfVehicle == null)
            {
                return NotFound();
            }

            return View(typeOfVehicle);
        }

        // POST: TypeOfVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfVehicle = await _context.TypeOfVehicle.FindAsync(id);
            _context.TypeOfVehicle.Remove(typeOfVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfVehicleExists(int id)
        {
            return _context.TypeOfVehicle.Any(e => e.Id == id);
        }
    }
}
