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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Member.ToListAsync());
        }

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
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Phone,DateOfBirth,Age,PersonalIdNumber,DateAdded,BonusAccountExpires")] Member member)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phone,DateOfBirth,Age,PersonalIdNumber,DateAdded,BonusAccountExpires")] Member member)
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
        //[AcceptVerbs("GET", "POST")]
        //public IActionResult IsRegExists(string CodeNum, int Id)
        //{
        //    return Json(IsUnique(CodeNum, Id));
        //}


        public async Task<IActionResult> MemberCheckIn(int id)
        {
            var member = _context.Member.Find(id);
            var vehicles = _context.ParkedVehicle;
            var memberVehicles = await vehicles.Where(v => v.MemberId == member.Id).ToListAsync();
           // member.MemberVehicles = memberVehicles;
            return View("MemberCheckIn", member);
        }

        public async Task<IActionResult> CheckOutConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            parkedVehicle.IsInGarage = false;
            parkedVehicle.EnteringTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CheckInConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            parkedVehicle.IsInGarage = true;
            parkedVehicle.EnteringTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult CreateNewVehicle(int id)
        //{
        //    var model = new ParkedVehicle
        //    {
        //        GetVehiclesType = GetTypeOfVehicle()
        //    };
        //    model.MemberId = id;

        ////private bool IsUnique(string CodeNum, int Id)
        //{
        //    if (Id == 0) // its a new object
        //    {
        //        return !_context.ParkedVehicle.Any(x => x.RegistrationNum == CodeNum);
        //    }
        //    else
        //    {
        //        return !_context.ParkedVehicle.Any(x => x.RegistrationNum == CodeNum && x.Id != Id);
        //    }
        //}

        public bool IsCodeNumberExists(string PersonalIdNumber, int Id)
        {
            if (Id == 0)
            {
                var members = _context.Member.Any(x => x.PersonalIdNumber == PersonalIdNumber);
               
                return !_context.Member.Any(x => x.PersonalIdNumber == PersonalIdNumber);
            }
            else
            {
                return !_context.Member.Any(x => x.PersonalIdNumber == PersonalIdNumber && x.Id != Id);

            }
        }
    }
}
