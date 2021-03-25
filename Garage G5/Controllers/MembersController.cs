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
    public class MembersController : Controller
    {
        private readonly Garage_G5Context _context;

        public MembersController(Garage_G5Context context)
        {
            _context = context;
        }

        // GET: Members
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Member.ToListAsync());
        //}

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Phone,Age,PersonalIdNumber,DateAdded,BonusAccountExpires,MembershipType")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.DateAdded = DateTime.Now;
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phone,Age,PersonalIdNumber,DateAdded,BonusAccountExpires,MembershipType")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    _context.Entry(member).Property(x => x.DateAdded).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.FullNameSortParm = (sortOrder == "FullName") ? $"{sortOrder}_desc" : "FullName";
            ViewBag.AgeSortParm = (sortOrder == "Age") ? $"{sortOrder}_desc" : "Age";
            ViewBag.DateAddedSortParm = (sortOrder == "DateAdded") ? $"{sortOrder}_desc" : "DateAdded";
            ViewBag.MembershipTypeSortParm = (sortOrder == "MembershipType") ? $"{sortOrder}_desc" : "MembershipType";
            ViewBag.PersonalIdNumberSortParm = (sortOrder == "PersonalIdNumber") ? $"{sortOrder}_desc" : "PersonalIdNumber";

            var vehicles = from v in _context.Member
                           select v;
            switch (sortOrder)
            {
                case "FullName":
                    vehicles = vehicles.OrderBy(v => v.FullName);
                    break;
                case "FullName_desc":
                    vehicles = vehicles.OrderByDescending(v => v.FullName);
                    break;

                case "Age":
                    vehicles = vehicles.OrderBy(v => v.Age);
                    break;
                case "Age_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Age);
                    break;
                case "DateAdded":
                    vehicles = vehicles.OrderBy(v => v.DateAdded);
                    break;
                case "DateAdded_desc":
                    vehicles = vehicles.OrderByDescending(v => v.DateAdded);
                    break;
                case "MembershipType":
                    vehicles = vehicles.OrderBy(v => v.MembershipType);
                    break;
                case "MembershipType_desc":
                    vehicles = vehicles.OrderByDescending(v => v.MembershipType);
                    break; 
                case "PersonalIdNumber":
                    vehicles = vehicles.OrderBy(v => v.PersonalIdNumber);
                    break;
                case "PersonalIdNumber_desc":
                    vehicles = vehicles.OrderByDescending(v => v.PersonalIdNumber);
                    break;          
              

            }

            //var geniral = mapper.ProjectTo<GeneralInfoViewModel>(vehicles);

            //var list = new VehicleFilterViewModel
            //{
            //    Types = await GetVehicleTypeAsync(),
            //    GenralVehicles = geniral.ToList()
            //};

            return View("Index", await vehicles.ToListAsync());
        }

    }
}
