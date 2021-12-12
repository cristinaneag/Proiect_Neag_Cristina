using Proiect_Neag_Cristina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Data
{
    public class DbInitializer
    {
        public static void Initialize(PerfumeStoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Perfumes.Any())
            {
                return; 
            }

            var perfumes = new Perfume[]
            {
                new Perfume{Name="La Vie Est Belle",Brand="LANCÔME",Price=Decimal.Parse("352"), Weight=Decimal.Parse("100") },
                new Perfume{Name="Scandal",Brand="Jean Paul Gaultier",Price=Decimal.Parse("251"), Weight=Decimal.Parse("50") },
                new Perfume{Name="Black Opium",Brand="Jean Paul Gaultier",Price=Decimal.Parse("379"), Weight=Decimal.Parse("90") },
                new Perfume{Name="Si",Brand="Armani",Price=Decimal.Parse("415"), Weight=Decimal.Parse("100") }            };
            foreach (Perfume s in perfumes)
            {
                context.Perfumes.Add(s);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{CustomerID=1050,Name="Avram Mihaela",BirthDate=DateTime.Parse("1990-12-21")},
                new Customer{CustomerID=1045,Name="Mihai Ovidiu",BirthDate=DateTime.Parse("1993-01-03")},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{PerfumeID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2020")},
                new Order{PerfumeID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{PerfumeID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
                new Order{PerfumeID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{PerfumeID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{PerfumeID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();

            var manufacturers = new Manufacturer[]
            {
                new Manufacturer{Name="Charrier",Cui="901783"},
                new Manufacturer{Name="Sozio",Cui="810937"},
                new Manufacturer{Name="Collomb",Cui="6390237"},
            };
            foreach (Manufacturer p in manufacturers)
            {
                context.Manufacturer.Add(p);
            }
            context.SaveChanges();

            var manufacturedperfumes = new ManufacturedPerfumes[]
            {
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "Black Opium" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Collomb").ID
                },
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "La Vie Est Belle" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Sozio").ID
                },
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "Si" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Sozio").ID
                },
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "Scandal" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Charrier").ID
                },
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "La Vie Est Belle" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Charrier").ID
                },
                new ManufacturedPerfumes {
                PerfumeID = perfumes.Single(c => c.Name == "Scandal" ).ID,
                ManufacturerID = manufacturers.Single(i => i.Name == "Charrier").ID
                },
            };
            foreach (ManufacturedPerfumes mf in manufacturedperfumes)
            {
                context.ManufacturedPerfumes.Add(mf);
            }
            context.SaveChanges();
        }
    }
}
