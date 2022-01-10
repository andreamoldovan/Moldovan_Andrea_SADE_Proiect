using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        [StringLength(50)]
        public string SupplierName { get; set; }

        [StringLength(70)]
        public string Address { get; set; }
        public ICollection<SuppliedCoffee> SuppliedCoffees { get; set; }
    }
}
