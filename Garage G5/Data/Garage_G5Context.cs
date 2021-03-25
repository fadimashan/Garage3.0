
using Microsoft.EntityFrameworkCore;
using Garage_G5.Models;

namespace Garage_G5.Data
{
    public class Garage_G5Context : DbContext
    {
        public Garage_G5Context (DbContextOptions<Garage_G5Context> options)
            : base(options)
        {
        }

        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<Garage_G5.Models.TypeOfVehicle> TypeOfVehicle { get; set; }

        public DbSet<Garage_G5.Models.Member> Member { get; set; }
    }
}
