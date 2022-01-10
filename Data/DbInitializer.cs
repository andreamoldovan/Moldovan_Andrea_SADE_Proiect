using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moldovan_Andrea_SADE_Proiect.Models;

namespace Moldovan_Andrea_SADE_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(CafeContext context)
        {
            context.Database.EnsureCreated();
            if (context.Coffees.Any())
            {
                return; // BD a fost creata anterior
            }
            var coffees = new Coffee[]
            {
                new Coffee{Name="Flat White",Taste="Silky-Creamy Testure",Weight=Decimal.Parse("150"),Price=Decimal.Parse("20")},
                new Coffee{Name="Frappuccino",Taste="Iced Coffee Blended With Milk",Weight=Decimal.Parse("170"),Price=Decimal.Parse("15")},
                new Coffee{Name="Espresso",Taste="Concentrated",Weight=Decimal.Parse("250"),Price=Decimal.Parse("22")},
                new Coffee{Name="Frappe",Taste="Light layer of foam",Weight=Decimal.Parse("500"),Price=Decimal.Parse("30")},
                new Coffee{Name="Latte Macchiato",Taste="Full-flavored Arabica Selection",Weight=Decimal.Parse("400"),Price=Decimal.Parse("27")},
                new Coffee{Name="Shakissimo",Taste="Deliciously milk-rich",Weight=Decimal.Parse("250"),Price=Decimal.Parse("15")},
                new Coffee{Name="Double Shot Espresso",Taste="Amplified—bitter, lightly sweet, toasty",Weight=Decimal.Parse("200"),Price=Decimal.Parse("20")},
                new Coffee{Name="Light Latte",Taste="Australian specialty",Weight=Decimal.Parse("150"),Price=Decimal.Parse("20")},
                new Coffee{Name="Cappuccino",Taste="luxuriously smooth, rich foam",Weight=Decimal.Parse("200"),Price=Decimal.Parse("10")},

            };
            foreach (Coffee c in coffees)
            {
                context.Coffees.Add(c);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

                 new Customer{CustomerID=100,Name="Stefanos Tsitsipas",BirthDate=DateTime.Parse("1998-08-12")},
                 new Customer{CustomerID=110,Name="Andrey Rublev",BirthDate=DateTime.Parse("1997-10-20")},
                 new Customer{CustomerID=120,Name="Sebastian Stan",BirthDate=DateTime.Parse("1982-08-13")},
                 new Customer{CustomerID=130,Name="Jake Gyllenhaal",BirthDate=DateTime.Parse("1980-12-19")},

            };
            foreach (Customer r in customers)
            {
                context.Customers.Add(r);
            }
            context.SaveChanges();
            var invoices = new Invoice[]
            {
                 new Invoice{CoffeeID=1,CustomerID=100},
                 new Invoice{CoffeeID=3,CustomerID=100},
                 new Invoice{CoffeeID=2,CustomerID=130},
                 new Invoice{CoffeeID=4,CustomerID=120},
                 new Invoice{CoffeeID=5,CustomerID=110},
                 new Invoice{CoffeeID=6,CustomerID=130},
                 new Invoice{CoffeeID=1,CustomerID=110},
                 new Invoice{CoffeeID=2,CustomerID=120},

            };
            foreach (Invoice e in invoices)
            {
                context.Invoices.Add(e);
            }
            context.SaveChanges();
            var suppliers = new Supplier[]
 {
                new Supplier{SupplierName="Kaiku",Address="Str. George Emil Palade, nr. 40, Oradea"},
                new Supplier{SupplierName="Mizo",Address="Str. Roman Ciorogariu, nr. 18, Oradea"},
                new Supplier{SupplierName="Nescafe",Address="Str. Valeriu Bologa, nr. 3, Cluj-Napoca"},
                new Supplier{SupplierName="Starbucks",Address="Str. Alexandru Vaida Voevod, nr. 53, Cluj-Napoca"},
                new Supplier{SupplierName="Alpro",Address="Str. Bulevard 21 Decembrie, nr. 25, Cluj-Napoca"},
                new Supplier{SupplierName="Milbona",Address="Str. Cascadelor, nr. 22, Cluj-Napoca"},
                new Supplier{SupplierName="Zuzu",Address="Str. Plopilor, nr. 22, Bucuresti"},
 };
            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();
            var suppliedcoffees = new SuppliedCoffee[]
            {
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Flat White" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName =="Mizo").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Frappuccino" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName =="Starbucks").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Shakissimo" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName =="Nescafe").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Double Shot Espresso" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Starbucks").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Latte Macchiato" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Alpro").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Espresso" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Kaiku").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Frappe" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Zuzu").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Light Latte" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Milbona").ID
             },
             new SuppliedCoffee {
             CoffeeID = coffees.Single(c => c.Name == "Cappuccino" ).ID,
             SupplierID = suppliers.Single(s => s.SupplierName == "Mizo").ID
             },

            };
            foreach (SuppliedCoffee sc in suppliedcoffees)
            {
                context.SuppliedCoffees.Add(sc);
            }
            context.SaveChanges();
        }
    }

}
