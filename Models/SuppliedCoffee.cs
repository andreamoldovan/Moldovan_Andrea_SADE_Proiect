using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class SuppliedCoffee
    {
        public int SupplierID { get; set; }
        public int CoffeeID { get; set; }
        public Supplier Supplier { get; set; }
        public Coffee Coffee { get; set; }
    }
}
