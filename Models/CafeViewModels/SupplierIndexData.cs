using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moldovan_Andrea_SADE_Proiect.Models.CafeViewModels
{
    public class SupplierIndexData
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Coffee> Coffees { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
