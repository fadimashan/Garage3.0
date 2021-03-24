using Bogus;
using Garage_G5.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Garage_G5.Data
{
    public static class SeedDate
    {
        private static Faker fake;
        public static async Task IntiAsync(IServiceProvider services)
        {
            using (var db = services.GetRequiredService<Garage_G5Context>())
            {
                if (db.ParkedVehicle.Any())
                {
                    return;

                }

                fake = new Faker("sv");
                List<ParkedVehicle> vehiclesList = GetVehicles();

                for (int i = 0; i < 100; i++)
                {
                    //if ()
                    //{

                    //}
                    try
                    {
                        await db.AddRangeAsync(vehiclesList[i]);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
        }

        private static List<ParkedVehicle> GetVehicles()
        {
            var vehicles = new List<ParkedVehicle>();
            
            DateTime startDateTime = new DateTime(2020, 01, 01);


            for (int i = 0; i < 101; i++)
            {

                var vehicle = new ParkedVehicle
                {
                    VehicleType = fake.PickRandom<VehicleType>(),
                    RegistrationNum = fake.Random.String2(6),
                    Color = fake.Commerce.Color(),
                    Brand = fake.Vehicle.Manufacturer(),
                    Model = fake.Vehicle.Model(),
                    WheelsNum = fake.Random.Int(0, 12),
                    EnteringTime = fake.Date.Between(startDateTime, DateTime.Now) 

                };

                vehicles.Add(vehicle);
            }

            return vehicles;
        }
    }
}