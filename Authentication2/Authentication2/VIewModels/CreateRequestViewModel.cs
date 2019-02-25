using Authentication2.Identity;
using Authentication2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication2.VIewModels
{
    public class CreateRequestViewModel
    {
        public CreateRequestViewModel(){ }

        public CreateRequestViewModel(RequestModel model)
        {
            Id = model.Id;
            PickupStreetName = model.PickupAddress.StreetName;
            PickupCity = model.PickupAddress.City;
            PickupState = model.PickupAddress.State;
            PickupInstructions = model.PickUpInstructions;
            PickupStreetNumber = model.PickupAddress.StreetNumber;
            PickupZipcode = model.PickupAddress.ZipCode;
            DropoffStreetName = model.DropOffAddress.StreetName;
            DropoffCity = model.DropOffAddress.City;
            DropoffState = model.DropOffAddress.State;
            DropoffInstructions = model.DropOffInstructions;
            DropoffStreetNumber = model.DropOffAddress.StreetNumber;
            DropoffZipcode = model.DropOffAddress.ZipCode;
            Item = model.Item;
        }

        public int Id { get; set; }
        public string PickupStreetName { get; set; }
        public string PickupCity { get; set; }
        public string PickupState { get; set; }
        public string PickupInstructions { get; set; }
        public string PickupStreetNumber { get; set; }
        public int PickupZipcode { get; set; }
        public string DropoffStreetName { get; set; }
        public string DropoffCity { get; set; }
        public string DropoffState { get; set; }
        public string DropoffInstructions { get; set; }
        public string DropoffStreetNumber { get; set; }
        public int DropoffZipcode { get; set; }
        public string Item { get; set; }
    }
}
