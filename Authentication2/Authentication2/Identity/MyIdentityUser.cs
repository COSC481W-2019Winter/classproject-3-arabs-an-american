using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.Identity
{
    public class MyIdentityUser : IdentityUser
    {

        // UNENCRYPTED PASSWORD FOR DEVELOPMENT PURPOSES ONLY
        // so we dont have to remember passwords
        public string Password { get; set; }

        public Address Address { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string CarYear { get; set; }
        public string LicensePlate { get; set; }

    }

    public class Address
    {
        public int Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
