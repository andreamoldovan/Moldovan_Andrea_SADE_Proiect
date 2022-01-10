using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Moldovan_Andrea_SADE_Proiect.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }

    }
}
