﻿using Authentication2.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class UserViewModel
    {
        public Address Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
