using Authentication2.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class SignUpViewModel
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

    }
}
