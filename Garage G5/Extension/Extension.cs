using AutoMapper;
using Garage_G5.Models;
using Garage_G5.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Extension
{
    public static class Extension
    {
    
        public static VehicleFilterViewModel GetFilterList(this DbSet<ParkedVehicle> listOfPV, IMapper mapper)
        {
            IQueryable<ParkedVehicle> v = listOfPV;
            var geniral = mapper.ProjectTo<GeneralInfoViewModel>(v);

            var list = new VehicleFilterViewModel
            {
                Types =  GetVehicleTypeAsync(listOfPV),
                GeneralVehicles = geniral.ToList()
            };

            return list;


        }

        private static IEnumerable<SelectListItem> GetVehicleTypeAsync(DbSet<ParkedVehicle> listOfPV)
        {
            return listOfPV
                .Select(p => p.VehicleType)
                .Distinct()
                .Select(g => new SelectListItem
                {
                    Text = g.ToString(),
                    Value = g.ToString(),
                })
                .ToList();
        }


    }


}
