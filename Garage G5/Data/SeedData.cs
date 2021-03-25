using Bogus;
using Garage_G5.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bogus.Extensions.Sweden;
using System.Text;

namespace Garage_G5.Data
{
    public static class SeedData
    {
        private static Faker fake;
        public static async Task InitAsync(IServiceProvider services)
        {
            using (var db = services.GetRequiredService<Garage_G5Context>())
            {
                //if (db.ParkedVehicle.Any() || db.Member.Any() || db.TypeOfVehicle.Any())
                //{
                //    return;
                //}

                fake = new Faker("sv");

                //Add Parked Vehicles
                List<ParkedVehicle> vehiclesList = GetVehicles();
                for (int i = 0; i < vehiclesList.Count(); i++)
                {
                    try
                    {
                        await db.AddRangeAsync(vehiclesList[i]);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {

                    }

                }

                //Add members
                List<Member> membersList = GetMembers(25);
                for (int i = 0; i < membersList.Count(); i++)
                {
                    await db.AddRangeAsync(membersList[i]);
                }
                await db.SaveChangesAsync();

                //Add Vehicle Types                
                List<TypeOfVehicle> typeOfVehiclesList = GetTypesOfVehicles();
                for (int i = 0; i < typeOfVehiclesList.Count(); i++)
                {
                    await db.AddRangeAsync(typeOfVehiclesList[i]);
                }
                await db.SaveChangesAsync();
            }


        }

        private static List<Member> GetMembers(int amount)
        {
            var members = new List<Member>();
            DateTime startDateTime = new DateTime(2020, 01, 01);
            for (int i = 0; i < amount; i++)
            {
                int first4digits = fake.Random.Int(1939, 2010);
                var personalIdNumber = new StringBuilder();
                personalIdNumber.Append(first4digits);
                personalIdNumber.Append(fake.Random.Int(11, 12));
                personalIdNumber.Append(fake.Random.Int(10, 28));
                personalIdNumber.Append(fake.Random.Int(0001, 9999));

                var member = new Member
                {
                    FirstName = fake.Name.FirstName(),
                    LastName = fake.Name.LastName(),
                    MembershipType = (MembershipType)0,
                    Phone = fake.Phone.PhoneNumberFormat(),
                    PersonalIdNumber = personalIdNumber.ToString(),
                    Age = DateTime.Now.Year - first4digits,
                    DateAdded = fake.Date.Between(startDateTime, DateTime.Now),
                    BonusAccountExpires = fake.Date.Between(startDateTime, DateTime.Now)
                };

                members.Add(member);
            }

            return members;
        }

        private static List<TypeOfVehicle> GetTypesOfVehicles()
        {
            var typesOfVehicles = new List<TypeOfVehicle>() {
                new TypeOfVehicle {
                    TypeName = "Motorcycle",
                    Size = 1,
                },
                new TypeOfVehicle{
                    TypeName = "Combi",
                    Size = 1,
                },
                new TypeOfVehicle{
                    TypeName = "Sedan",
                    Size = 1,
                },
                new TypeOfVehicle {
                    TypeName = "Coupe",
                    Size = 1,
                },
                new TypeOfVehicle{
                    TypeName = "Van",
                    Size = 1,
                },
                new TypeOfVehicle {
                    TypeName = "Roadster",
                    Size = 1,
                },
                new TypeOfVehicle{
                    TypeName = "MiniVan",
                    Size = 1,
                },
                new TypeOfVehicle{
                    TypeName = "Truck",
                    Size = 2,
                },
                new TypeOfVehicle{
                    TypeName = "BigTruck",
                    Size = 2,
                },
                new TypeOfVehicle{
                    TypeName = "Boat",
                    Size = 3,
                },
                new TypeOfVehicle{
                    TypeName = "Airplane",
                    Size = 3,
                },
            };
            return typesOfVehicles;
        }

        private static List<ParkedVehicle> GetVehicles()
        {
            var vehicles = new List<ParkedVehicle>();
            
            DateTime startDateTime = new DateTime(2020, 01, 01);


            for (int i = 0; i < 21; i++)
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