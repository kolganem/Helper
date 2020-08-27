using MyDiplom.Data;
using MyDiplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiplom.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context)
        {

            if (!context.Devices.Any())
            {
                context.Devices.AddRange(new List<Device>
                {
                    new Device { Stand = 101, PlaceOnStand = 11, typeDevice = "КПТШ-515", NumberDevice = 321, YearDevice = 1986, DateCheck = new DateTime(2015, 03, 14), DateFutureCheck = new DateTime(2025, 03, 14)},
                    new Device { Stand = 101, PlaceOnStand = 12, typeDevice = "НМШ2-4000", NumberDevice = 1221, YearDevice = 1988, DateCheck = new DateTime(2015, 10, 25), DateFutureCheck = new DateTime(2025, 10, 25)},
                    new Device { Stand = 101, PlaceOnStand = 13, typeDevice = "КБМШ-450", NumberDevice = 1010, YearDevice = 1989, DateCheck = new DateTime(2013, 02, 04), DateFutureCheck = new DateTime(2018, 02, 04)},
                    new Device { Stand = 102, PlaceOnStand = 31, typeDevice = "СП-69", NumberDevice = 3991, YearDevice = 1978, DateCheck = new DateTime(2018, 12, 11), DateFutureCheck = new DateTime(2020, 12, 11)},
                    new Device { Stand = 103, PlaceOnStand = 21, typeDevice = "ПС-220М", NumberDevice = 1231, YearDevice = 1985, DateCheck = new DateTime(2019, 01, 14), DateFutureCheck = new DateTime(2020, 01, 14)},

                });
                await context.SaveChangesAsync();

            }



        }

    }
}
