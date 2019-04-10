using Authentication2.Identity;
using Authentication2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Authentication2.VIewModels
{
    public class CreateRequestViewModel
    {
        public CreateRequestViewModel(){ }

        public CreateRequestViewModel(RequestModel model)
        {
            Id = model.Id;
            UserId = model.UserId;
            DriverId = model.DriverId;
            Status = model.Status;
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
            //Image = model.ImagePath
        }

        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DriverId { get; set; }
        public string Status { get; set; }
        [Required]
        public string PickupStreetNumber { get; set; }
        [Required]
        public string PickupStreetName { get; set; }
        [Required]
        public string PickupCity { get; set; }
        [Required]
        public string PickupState { get; set; }
        [Required]
        public int PickupZipcode { get; set; }
        [Required]
        public string DropoffStreetNumber { get; set; }
        [Required]
        public string DropoffStreetName { get; set; }
        [Required]
        public string DropoffCity { get; set; }
        [Required]
        public string DropoffState { get; set; }
        [Required]
        public int DropoffZipcode { get; set; }


        public string DropoffInstructions { get; set; }
        public string PickupInstructions { get; set; }

        [Required]
        public string Item { get; set; }

        public IFormFile Image { set; get; }
    }
}
