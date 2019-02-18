using Authentication2.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class CreateRequestViewModel
    {
        public string PickupStreetName { get; set; }
        public string PickupCity { get; set; }
        public string PickupState { get; set; }
        public string PickupInstructions { get; set; }
        public int PickupStreetNumber { get; set; }
        public int PickupZipcode { get; set; }
        public string DropoffStreetName { get; set; }
        public string DropoffCity { get; set; }
        public string DropoffState { get; set; }
        public string DropoffInstructions { get; set; }
        public int DropoffStreetNumber { get; set; }
        public int DropoffZipcode { get; set; }
        public string item { get; set; }
    }
}