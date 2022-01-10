using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moldovan_Andrea_SADE_Proiect.Models.CafeViewModels
{
    public class InvoiceGroup
    {
        [DataType(DataType.Date)]
        public DateTime? InvoiceDate { get; set; }
        public int CoffeeCount { get; set; }
    }
}
