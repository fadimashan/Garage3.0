using Bogus;
using Garage_G5.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Garage_G5.Data
{
    public static class SeedData
    {
        private static Faker fake;
        private static List<TypeOfVehicle> typesOfVehicles;
        private static List<Member> members;
        private static bool underagePerson;
        public static async Task InitAsync(IServiceProvider services)
        {
            using (var db = services.GetRequiredService<Garage_G5Context>())
            {
                if (db.ParkedVehicle.Any() || db.Member.Any() || db.TypeOfVehicle.Any())
                {
                    return;
                }

                fake = new Faker("sv");

                //Add members
                List<Member> membersList = GetMembers(25);
                for (int i = 0; i < membersList.Count(); i++)
                {
                    await db.AddRangeAsync(membersList[i]);
                }

                //Add Vehicle Types                
                List<TypeOfVehicle> typeOfVehiclesList = GetTypesOfVehicles();
                for (int i = 0; i < typeOfVehiclesList.Count(); i++)
                {
                    await db.AddRangeAsync(typeOfVehiclesList[i]);
                }

                //Add Parked Vehicles
                List<ParkedVehicle> vehiclesList = GetVehicles(21);
                for (int i = 0; i < vehiclesList.Count(); i++)
                {
                    await db.AddRangeAsync(vehiclesList[i]);
                }

                //Update the DB
                await db.SaveChangesAsync();
            }


        }

        private static List<Member> GetMembers(int amount)
        {
            members = new List<Member>();
            DateTime startDateTime = new DateTime(2020, 01, 01);
            for (int i = 0; i < amount; i++)
            {
                int first4digits = fake.Random.Int(1939, 2010);
                var month = fake.Random.Int(11, 12);
                var day = fake.Random.Int(10, 28);
                var personalIdNumber = new StringBuilder();
                personalIdNumber.Append(first4digits);
                personalIdNumber.Append(month);
                personalIdNumber.Append(day);
                personalIdNumber.Append(fake.Random.Int(0001, 9999));
                underagePerson = DateTime.Now.Year - first4digits < 18 ? true : false;

                var member = new Member
                {
                    FirstName = fake.Name.FirstName(),
                    LastName = fake.Name.LastName(),
                    MembershipType = (MembershipType)0,
                    Phone = fake.Phone.PhoneNumberFormat(),
                    PersonalIdNumber = personalIdNumber.ToString(), //198010101234
                    Age = DateTime.Now.Year - first4digits,
                    DateAdded = fake.Date.Between(startDateTime, DateTime.Now),
                    BonusAccountExpires = fake.Date.Between(startDateTime, DateTime.Now),
                    DateOfBirth = new DateTime(first4digits,month, day),
                    TotalParkedTime = fake.Random.Double(0, 300),
                    IsUnderage = underagePerson
                };

                members.Add(member);
            }

            return members;
        }

        private static List<TypeOfVehicle> GetTypesOfVehicles()
        {
            typesOfVehicles = new List<TypeOfVehicle>() {
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

        private static List<ParkedVehicle> GetVehicles(int amount)
        {
            var vehicles = new List<ParkedVehicle>();

            DateTime startDateTime = new DateTime(2020, 01, 01);

            for (int i = 0; i < amount; i++)
            {

                var vehicle = new ParkedVehicle
                {
                    RegistrationNum = fake.Random.AlphaNumeric(6),
                    Color = fake.Commerce.Color(),
                    Brand = fake.Vehicle.Manufacturer(),
                    Model = fake.Vehicle.Model(),
                    WheelsNum = 4,
                    EnteringTime = fake.Date.Between(startDateTime, DateTime.Now),
                    TypeOfVehicle = fake.Random.ListItem<TypeOfVehicle>(typesOfVehicles),
                    Member = fake.Random.ListItem<Member>(members),
                    IsInGarage = fake.Random.Bool()
                };

                vehicles.Add(vehicle);
            }

            return vehicles;
        }
    }
}