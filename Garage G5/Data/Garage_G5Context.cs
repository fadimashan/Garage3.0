﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Models;
using Garage_G5.ViewModels;

namespace Garage_G5.Data
{
    public class Garage_G5Context : DbContext
    {
        public Garage_G5Context (DbContextOptions<Garage_G5Context> options)
            : base(options)
        {
        }

        public DbSet<Garage_G5.Models.ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<Garage_G5.ViewModels.ReceiptModel> ReceiptModel { get; set; }
    }
}
