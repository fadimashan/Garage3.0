using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Data;
using Garage_G5.Models;
using AutoMapper;
using Garage_G5.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Garage_G5.Controllers
{
    public class MembersController : Controller
    {
        private readonly Garage_G5Context _context;
        private readonly IMapper mapper;


        public MembersController(Garage_G5Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Members
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Member.ToListAsync());
        //}

        public async Task<IActionResult> Index(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber, 
            int? userPageSize)
        {


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            var members = from v in _context.Member
                           select v;

            int pageSize = 5;
            if(userPageSize != null)
            {
                pageSize = (int)userPageSize;
            }

            //members = string.IsNullOrWhiteSpace(FullName) ?
            //   _context.Member :
            //   _context.Member.Where(v => v.FirstName.StartsWith(FullName) || v.LastName.StartsWith(FullName));

            ViewBag.FullNameSortParm = (sortOrder == "FullName") ? $"{sortOrder}_desc" : "FullName";
            ViewBag.AgeSortParm = (sortOrder == "Age") ? $"{sortOrder}_desc" : "Age";
            ViewBag.DateAddedSortParm = (sortOrder == "DateAdded") ? $"{sortOrder}_desc" : "DateAdded";
            ViewBag.MembershipTypeSortParm = (sortOrder == "MembershipType") ? $"{sortOrder}_desc" : "MembershipType";
            ViewBag.PersonalIdNumberSortParm = (sortOrder == "PersonalIdNumber") ? $"{sortOrder}_desc" : "PersonalIdNumber";
            ViewBag.DateExpiredSortParm = (sortOrder == "DateExpired") ? $"{sortOrder}_desc" : "DateExpired";

            switch (sortOrder)
            {
                case "FullName":
                    members = members.OrderBy(v => v.FullName);
                    break;
                case "FullName_desc":
                    members = members.OrderByDescending(v => v.FullName);
                    break;

                case "Age":
                    members = members.OrderBy(v => v.Age);
                    break;
                case "Age_desc":
                    members = members.OrderByDescending(v => v.Age);
                    break;
                case "DateAdded":
                    members = members.OrderBy(v => v.DateAdded);
                    break;
                case "DateAdded_desc":
                    members = members.OrderByDescending(v => v.DateAdded);
                    break;
                case "MembershipType":
                    members = members.OrderBy(v => v.MembershipType);
                    break;
                case "MembershipType_desc":
                    members = members.OrderByDescending(v => v.MembershipType);
                    break;
                case "PersonalIdNumber":
                    members = members.OrderBy(v => v.PersonalIdNumber);
                    break;
                case "PersonalIdNumber_desc":
                    members = members.OrderByDescending(v => v.PersonalIdNumber);
                    break;
                case "DateExpired":
                    members = members.OrderBy(v => v.BonusAccountExpires);
                    break;
                case "DateExpired_desc":
                    members = members.OrderByDescending(v => v.BonusAccountExpires);
                    break;
            }
            //return View(/*await PaginatedList<Member>.CreateAsync(members.AsNoTracking(), pageNumber ?? 1, pageSize)*/);

            return View(await PaginatedList<Member>.CreateAsync(members.AsNoTracking(), pageNumber ?? 1, pageSize));
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


        public async Task<IActionResult> MemberCheckIn(int id)
        {
            var member = _context.Member.Find(id);
            var vehicles = _context.ParkedVehicle;
            var memberVehicles = await vehicles.Where(v => v.MemberId == member.Id).ToListAsync();
            member.MemberVehicles = memberVehicles;
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

        public IActionResult CreateNewVehicle(int id)
        {
            var model = new ParkedVehicle
            {
                GetVehiclesType = GetTypeOfVehicle()
            };
            model.MemberId = id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewVehicle([Bind("VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime,MemberId")] ParkedVehicle parkedVehicle)
        {

            if (ModelState.IsValid)
            {
                parkedVehicle.EnteringTime = DateTime.Now;
                parkedVehicle.IsInGarage = false;
                _context.ParkedVehicle.Add(parkedVehicle);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

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
                });
                GetTypeOfVehicle.Add(newType);
            }
            return (GetTypeOfVehicle);
        }

        public async Task<IActionResult> Search(string FullName)
        {
            var membes = string.IsNullOrWhiteSpace(FullName) ?
            _context.Member :
            _context.Member.Where(v => v.FirstName.StartsWith(FullName) || v.LastName.StartsWith(FullName));
            return View("Index", await membes.ToListAsync());
        }

        private async Task<IEnumerable<SelectListItem>> GetVehicleTypeAsync()
        {
            var g = _context.ParkedVehicle;
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

    }
}
