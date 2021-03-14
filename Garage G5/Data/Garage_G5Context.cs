using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Models;
using Garage_G5.ViewModels;
using Garage_G5.Models.ViewModels;

namespace Garage_G5.Data
{
    public class Garage_G5Context : DbContext
    {
        public Garage_G5Context (DbContextOptions<Garage_G5Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }
    }
}
