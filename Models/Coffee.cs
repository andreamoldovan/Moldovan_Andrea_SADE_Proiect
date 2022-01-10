using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class Coffee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Taste { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<SuppliedCoffee> SuppliedCoffees { get; set; }
    }
}
