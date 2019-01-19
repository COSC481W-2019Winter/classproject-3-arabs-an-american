using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.Identity
{
    public class MyIdentityUser : IdentityUser
    {
        public string LicensePlate { get; set; }
        public string Address { get; set; }
    }
}
