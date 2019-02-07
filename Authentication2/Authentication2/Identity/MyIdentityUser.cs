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

        // UNENCRYPTED PASSWORD FOR DEVELOPMENT PURPOSES ONLY
        // so we dont have to remember passwords
        public string Password { get; set; }
    }
}
