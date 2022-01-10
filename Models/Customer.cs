using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
