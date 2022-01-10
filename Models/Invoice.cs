using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }
        public int CoffeeID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Customer Customer { get; set; }
        public Coffee Coffee { get; set; }
    }
}
